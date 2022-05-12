using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Newtonsoft.Json;
using PersonalAssistantBot.Models;

namespace PersonalAssistantBot.Dialogs
{
    public class MainDialog : ComponentDialog
    {
        private readonly CardData _cardData;
        private readonly IRepository _repository;
        private readonly UserState _userState;
        public MainDialog(CardData cardData, IRepository repository, UserState userState)
            : base(nameof(MainDialog))
        {
            _cardData = cardData;
            _repository = repository;
            _userState = userState;

            AddDialog(new AdaptiveCardPrompt(nameof(AdaptiveCardPrompt)));

            var waterfallSteps = new WaterfallStep[]
            {
                SearchStepAsync,
                ChangeLanguageStepAsync,
                SearchResultsStepAsync,
                PaginationSearchResultsStepAsync,
                EmployeeInfoStepAsync,
            };

            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), waterfallSteps));
            AddDialog(new TextPrompt(nameof(TextPrompt)));

            InitialDialogId = nameof(WaterfallDialog);

        }
        private async Task<DialogTurnResult> SearchStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if (stepContext.Context != null && stepContext.Context.Activity != null && stepContext.Context.Activity.Value != null)
            {
                var userResponse = JsonConvert.DeserializeObject<SearchResultsValueJSON>(stepContext.Context.Activity.Value.ToString());
                if (userResponse.Value == "ChangePage" || userResponse.Value == "OpenProfile")
                {
                    return await stepContext.NextAsync(stepContext.Context.Activity.Value);
                }
            }

            var cardAttachment = await CardBuilder.CreateAdaptiveCardAttachment("SearchCard.json", _cardData.GetSearchCard());
            return await stepContext.PromptAsync(nameof(AdaptiveCardPrompt),
              new PromptOptions
              {
                  Prompt = (Activity)MessageFactory.Attachment(cardAttachment)
              }, cancellationToken);
        }
        private async Task<DialogTurnResult> ChangeLanguageStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if (stepContext.Result != null)
            {
                var userResponse = JsonConvert.DeserializeObject<SearchValueJSON>(stepContext.Result.ToString()).Value;

                if (userResponse == "ChangeLanguage")
                {
                    var userStateAccessors = _userState.CreateProperty<UserProfile>(nameof(UserProfile));
                    var userProfile = await userStateAccessors.GetAsync(stepContext.Context, () => new UserProfile());
                    userProfile.Language = userProfile.Language == "en" ? "ru" : "en";

                    CultureInfo.CurrentUICulture = new CultureInfo(userProfile.Language);
                    CultureInfo.CurrentCulture = new CultureInfo(userProfile.Language);

                    await _userState.SaveChangesAsync(stepContext.Context, false, cancellationToken);
                    return await stepContext.ReplaceDialogAsync(nameof(MainDialog));
                }

                return await stepContext.NextAsync(stepContext.Result, cancellationToken);
            }

            return await stepContext.NextAsync();
        }
        private async Task<DialogTurnResult> SearchResultsStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if (stepContext.Result != null)
            {
                var userResponse = JsonConvert.DeserializeObject<SearchValueJSON>(stepContext.Result.ToString()).Value;

                if (userResponse == "ChangePage" || userResponse == "OpenProfile")
                {
                    return await stepContext.NextAsync(stepContext.Result, cancellationToken);
                }
            }

            int pageNumber = 1;

            var cardAttachment = await CardBuilder.CreateAdaptiveCardAttachment("SearchResultsCard.json", _cardData.GetSearchResultsCard(_repository.GetSearchResultsPayload(pageNumber)));
            
            return await stepContext.PromptAsync(nameof(AdaptiveCardPrompt),
             new PromptOptions
             {
                 Prompt = (Activity)MessageFactory.Attachment(cardAttachment)
             }, cancellationToken);
        }

        private async Task<DialogTurnResult> PaginationSearchResultsStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if (stepContext.Result != null)
            {
                var userResponse = JsonConvert.DeserializeObject<SearchResultsValueJSON>(stepContext.Result.ToString());
                if (userResponse.Value == "OpenProfile")
                {
                    return await stepContext.NextAsync(stepContext.Result, cancellationToken);
                }
                if (userResponse.Value == "ChangePage")
                {
                    int pageNumber = Int32.Parse(userResponse.PageNumber);
                    var cardAttachment = await CardBuilder.CreateAdaptiveCardAttachment("SearchResultsCard.json", _cardData.GetSearchResultsCard(_repository.GetSearchResultsPayload(pageNumber)));
                    Activity activity = (Activity)MessageFactory.Attachment(cardAttachment);
                    activity.Id = stepContext.Context.Activity.ReplyToId;
                    await stepContext.Context.UpdateActivityAsync(activity);

                    return await stepContext.EndDialogAsync();
                }
            }

            return await stepContext.NextAsync();
        }

        private async Task<DialogTurnResult> EmployeeInfoStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if (stepContext.Result != null)
            {
                var userResponse = JsonConvert.DeserializeObject<EmployeeInfoValueJSON>(stepContext.Result.ToString());
                if (userResponse.Value == "OpenProfile")
                {
                    var wwid = userResponse.WWID;
                    var cardAttachment = await CardBuilder.CreateAdaptiveCardAttachment("EmployeeInfoCard.json", _cardData.GetEmployeeInfoCard(_repository.GetEmployeeByWwid(wwid)));
                    await stepContext.Context.SendActivityAsync(MessageFactory.Attachment(cardAttachment), cancellationToken);
                }
            }

            return await stepContext.EndDialogAsync();
        }
    }
}

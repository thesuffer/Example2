using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using PersonalAssistantBot.Models;
using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace PersonalAssistantBot.Bots
{
    public class PersonalAssistantBot<T> : ActivityHandler where T : Dialog
    {
        private readonly UserState _userState;

        protected readonly BotState ConversationState;
        protected readonly Dialog Dialog;

        public PersonalAssistantBot(UserState userState, T dialog, ConversationState conversationState)
        {
            _userState = userState;
            Dialog = dialog;
            ConversationState = conversationState;
        }
        public override async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default)
        {
            var userStateAccessors = _userState.CreateProperty<UserProfile>(nameof(UserProfile));
            var userProfile = await userStateAccessors.GetAsync(turnContext, () => new UserProfile());

            CultureInfo.CurrentUICulture = new CultureInfo(userProfile.Language);
            CultureInfo.CurrentCulture = new CultureInfo(userProfile.Language);

            await base.OnTurnAsync(turnContext, cancellationToken);

            await ConversationState.SaveChangesAsync(turnContext, false, cancellationToken);
            await _userState.SaveChangesAsync(turnContext, false, cancellationToken);

        }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            await this.SendTypingIndicatorAsync(turnContext).ConfigureAwait(false);
            await Dialog.RunAsync(turnContext, ConversationState.CreateProperty<DialogState>("DialogState"), cancellationToken);
        }
        private async Task SendTypingIndicatorAsync(ITurnContext turnContext)
        {
            try
            {
                var typingActivity = turnContext.Activity.CreateReply();
                typingActivity.Type = ActivityTypes.Typing;
                await turnContext.SendActivityAsync(typingActivity);
            }
            catch (Exception e)
            {

            }
        }
    }
}

using Microsoft.Bot.Schema;
using AdaptiveCards.Templating;

using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace PersonalAssistantBot.Models
{
    public static class CardBuilder
    {
        public static async Task<Attachment> CreateAdaptiveCardAttachment(string cardName, object jsonData = null)
        {
            string filePath = Path.Combine(".", "AdaptiveCards", cardName);
            var adaptiveCardJson = File.ReadAllText(filePath);
            AdaptiveCardTemplate template = new AdaptiveCardTemplate(adaptiveCardJson);

            string cardJson = template.Expand(jsonData);

            var adaptiveCardAttachment = new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(cardJson),
            };

            return adaptiveCardAttachment;
        }
    }
}

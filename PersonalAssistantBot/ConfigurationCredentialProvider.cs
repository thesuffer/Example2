using Microsoft.Bot.Connector.Authentication;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalAssistantBot
{
    public class ConfigurationCredentialProvider : SimpleCredentialProvider
    {
        public ConfigurationCredentialProvider(IConfiguration configuration)
            : base(configuration != null ? configuration["MicrosoftAppId"] : string.Empty, configuration != null ? configuration["MicrosoftAppPassword"] : string.Empty)
        {
        }
    }
}

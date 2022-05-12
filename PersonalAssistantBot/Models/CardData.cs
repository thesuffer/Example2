using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Localization;
using PersonalAssistantBot.Entities;
using System;
using System.Globalization;
using System.IO;

namespace PersonalAssistantBot.Models
{
    public class CardData
    {
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IHostingEnvironment _env;

        public CardData(IStringLocalizer<SharedResource> sharedLocalizer, IHostingEnvironment env)
        {
            _sharedLocalizer = sharedLocalizer;
            _env = env;
        }
        public object GetSearchCard()
        {
            var languageIamgePath = CultureInfo.CurrentUICulture.ToString() == "ru" ?
                _env.WebRootFileProvider.GetFileInfo("images/russian-flag.png")?.PhysicalPath :
                _env.WebRootFileProvider.GetFileInfo("images/english-flag.png")?.PhysicalPath;


            byte[] languageImageBytes = File.ReadAllBytes(languageIamgePath);
            string languageImageData = "data:image / png; base64," + Convert.ToBase64String(languageImageBytes);

            return new
            {
                Localization = new
                {
                    Placeholder = _sharedLocalizer["SearchCard_Placeholder"].ToString(),
                    SerchButtonText = _sharedLocalizer["SearchCard_SerchButtonText"].ToString(),
                    ValidationError = _sharedLocalizer["SearchCard_ValidationError"].ToString()
                },
                LanguageIcon = languageImageData
            };
        }

        public object GetSearchResultsCard(SearchResultsPayload searchResultsPayload)
        {
            return new
            {
                Localization = new
                {
                    MatchesFound = _sharedLocalizer["SearchResultsCard_MatchesFound"].ToString(),
                    Mob = _sharedLocalizer["SearchResultsCard_Mob"].ToString(),
                    OpenProfile = _sharedLocalizer["SearchResultsCard_OpenProfile"].ToString(),
                    Page = _sharedLocalizer["SearchResultsCard_Page"].ToString(),
                    Of = _sharedLocalizer["SearchResultsCard_Of"].ToString()
                },
                Payload = searchResultsPayload
            };
        }

        public object GetEmployeeInfoCard(EmployeeInfoPayload employeeInfoPayload)
        {
            return new
            {
                Localization = new
                {
                    FullName = _sharedLocalizer["EmployeeInfoCard_FullName"].ToString(),
                    FullNameEn = _sharedLocalizer["EmployeeInfoCard_FullNameEn"].ToString(),
                    Position = _sharedLocalizer["EmployeeInfoCard_Position"].ToString(),
                    Sector = _sharedLocalizer["EmployeeInfoCard_Sector"].ToString(),
                    Departament = _sharedLocalizer["EmployeeInfoCard_Departament"].ToString(),
                    Ext = _sharedLocalizer["EmployeeInfoCard_Ext"].ToString(),
                    Mobile = _sharedLocalizer["EmployeeInfoCard_Mobile"].ToString(),
                    City = _sharedLocalizer["EmployeeInfoCard_City"].ToString(),
                    CostCenter = _sharedLocalizer["EmployeeInfoCard_CostCenter"].ToString(),
                    Status = _sharedLocalizer["EmployeeInfoCard_Status"].ToString(),
                    Manager = _sharedLocalizer["EmployeeInfoCard_Manager"].ToString(),
                    BackButtonText = _sharedLocalizer["EmployeeInfoCard_BackButton"].ToString(),

                },

                Employee = employeeInfoPayload
            };
        }
    }
}

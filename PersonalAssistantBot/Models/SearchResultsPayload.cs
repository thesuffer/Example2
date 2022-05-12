using System.Collections.Generic;

namespace PersonalAssistantBot.Models
{
    public class SearchResultsPayload
    {
        public SearchResultsPayload(List<EmployeePayload> employees, int pageNumber, int totalResults)
        {
            Employees = employees;
            ResultsCount = totalResults;

            PagesCount = ResultsCount % 4 == 0 ? ResultsCount / 4 : ResultsCount / 4 + 1;
            PageNumber = pageNumber <= PagesCount ? pageNumber : PagesCount;
            
            if (PageNumber <= 4)
            {
                FirstPage = 1;
                LastPage = PagesCount < 4 ? PagesCount : 4;
            }
            else
            {
                int fullPages = PagesCount / 4;
                if (PageNumber > fullPages * 4)
                {
                    FirstPage = fullPages * 4 + 1;
                    LastPage = PagesCount;
                }
                else if (PageNumber == PagesCount)
                {
                    FirstPage = 4 * (PageNumber / 4 - 1) + 1;
                    LastPage = 4 * (PageNumber / 4);
                }
                else
                {
                    FirstPage = 4 * (PageNumber / 4) + 1;
                    LastPage = 4 * (PageNumber / 4 + 1);
                }
            }
        }

        public int ResultsCount { get; }
        public List<EmployeePayload> Employees { get; }
        public int PagesCount { get; }
        public int PageNumber { get; }
        public int FirstPage { get; }
        public int LastPage { get; }
    }

    public class EmployeePayload
    {
        public int? Id { get; set; }
        public string Image { get; set; }
        public string NameRU { get; set; }
        public string NameEN { get; set; }
        public string PositionName { get; set; }
        public string Ext { get; set; }
        public string Mob { get; set; }

    }
}

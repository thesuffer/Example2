namespace PersonalAssistantBot.Models
{
    public class ValueJSON
    {
        public string Value { get; set; }
    }

    public class SearchValueJSON: ValueJSON
    {
    }

    public class SearchResultsValueJSON
    {
        public string Value { get; set; }
        public string PageNumber { get; set; }
    }
    
    public class EmployeeInfoValueJSON: ValueJSON
    {
        public string WWID { get; set; }
    }
}

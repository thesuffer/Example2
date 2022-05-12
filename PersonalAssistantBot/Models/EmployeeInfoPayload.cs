using PersonalAssistantBot.Entities;

namespace PersonalAssistantBot.Models
{
    public class EmployeeInfoPayload
    {
        public EmployeeInfoPayload(BossemployeeInfoAll employee, BossemployeeInfoAll manager)
        {
            FullName = employee.LastNameRu + " " + employee.FirstNameRu + " " + employee.MiddleNameRu;
            FullNameEN = employee.LastName + " " + employee.FirstName;
            WWID = employee.Wwid;
            PositionName = employee.Positionname;
            Sector = employee.Sector;
            Departament = employee.Departmentname;
            Email = employee.BusinessEmail;
            Ext = employee.BusinessPhone;
            MobilePhone = employee.BusinessMobile;
            City = employee.Cityname;
            CostCenter = employee.Costcentre;
            Status = employee.Status;
            LineManager = manager.LastNameRu + " " + manager.FirstNameRu + " " + manager.MiddleNameRu;
        }

        public string FullName { get; set; }
        public string FullNameEN { get; set; }
        public string WWID { get; set; }
        public string PositionName { get; set; }
        public string Sector { get; set; }
        public string Image { get; set; }
        public string Departament { get; set; }
        public string Email { get; set; }
        public string Ext { get; set; }
        public string MobilePhone { get; set; }
        public string City { get; set; }
        public string CostCenter { get; set; }
        public string Status { get; set; }
        public string LineManager { get; set; }
    }
}

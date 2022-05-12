using System;
using System.Collections.Generic;

#nullable disable

namespace PersonalAssistantBot.Entities
{
    public partial class BossemployeeInfoAll
    {
        public int? Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int? Managersid { get; set; }
        public string FirstNameRu { get; set; }
        public string MiddleNameRu { get; set; }
        public string LastNameRu { get; set; }
        public string Wwid { get; set; }
        public string BusinessPhone { get; set; }
        public string BusinessMobile { get; set; }
        public string BusinessEmail { get; set; }
        public string Positionname { get; set; }
        public string Positionnameru { get; set; }
        public string Costcentre { get; set; }
        public string Employeetype { get; set; }
        public string Departmentname { get; set; }
        public string Departmentnameru { get; set; }
        public string Cityname { get; set; }
        public string Networkaccount { get; set; }
        public int? Departmentid { get; set; }
        public string Status { get; set; }
        public string Sectorru { get; set; }
        public string Sector { get; set; }
        public byte[] Photo { get; set; }
    }
}

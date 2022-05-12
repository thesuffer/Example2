using Microsoft.EntityFrameworkCore;
using PersonalAssistantBot.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonalAssistantBot.Models
{
    public interface IRepository
    {
        List<BossemployeeInfoAll> GetAll();
        IEnumerable<BossemployeeInfoAll> GetUsers(string userSearchValue);
        SearchResultsPayload GetSearchResultsPayload(int pageNumber);
        EmployeeInfoPayload GetEmployeeByWwid(string wwid);
    }

    public class Repository : IRepository
    {
        ApplicationDbContext _context;
        public Repository()
        {
            _context = new ApplicationDbContext();
        }
        public List<BossemployeeInfoAll> GetAll()
        {
            return _context.BossemployeeInfoAlls.ToList();
        }
        public IEnumerable<BossemployeeInfoAll> GetUsers (string userSearchValue)
        {
            return _context.BossemployeeInfoAlls.Where(b => 
            (new[] { "постоянно", "Временно по ШР", "сотрудники кадрового агентства" }.Contains(b.Employeetype) ||
                (b.Employeetype == "по договору подряда" && b.Wwid != null)) &&
                (b.Networkaccount.Contains($"{userSearchValue}") ||
                 b.Wwid.Contains($"{userSearchValue}") ||
                 EF.Functions.Like(b.LastName + ' ' + b.FirstName, $"%{userSearchValue}%") ||
                 EF.Functions.Like(b.FirstName + ' ' + b.LastName, $"%{userSearchValue}%") ||
                 EF.Functions.Like(b.LastNameRu + ' ' + b.FirstNameRu + ' ' + b.MiddleNameRu, $"%{userSearchValue}%") ||
                 EF.Functions.Like(b.FirstNameRu + ' ' + b.LastNameRu + ' ' + b.MiddleNameRu, $"%{userSearchValue}%")
             )).ToList(); 
        }

        public SearchResultsPayload GetSearchResultsPayload(int pageNumber)
        {
            var employees = new List<EmployeePayload>();
            // To do - load list of employees

            var searchResultsPayload = new SearchResultsPayload(employees, pageNumber, 40);

            return searchResultsPayload;
        }

        public EmployeeInfoPayload GetEmployeeByWwid(string wwid)
        {
            var employee = _context.BossemployeeInfoAlls.FirstOrDefault(e => e.Wwid == wwid);
            var manager = _context.BossemployeeInfoAlls.FirstOrDefault(m => m.Id == employee.Managersid);

            return new EmployeeInfoPayload(employee, manager);
        }
    }
}

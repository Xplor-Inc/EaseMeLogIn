using EaseLogMeIn.DatabaseLayer;
using System;
using System.Collections.Generic;
using EaseLogMeIn.Models;

namespace EaseLogMeIn.BusinessLayer
{
    public interface IEmployeeManager
    {
        int Add(Employee employee, out Guid UserId);
        int Add(Employee employee);
        int Update(Employee employee);
        int Delete(Guid employeeId);
        List<Employee> Get(string Name, int PageSize, int PageIndex, out int Count);
        int Block(Guid UniqueId);
        int UnBlock(Guid UniqueId);
        List<MappingData> GetMapping();
        Employee IsValidEmployee(string UserId);
    }

    public class EmployeeManager : IEmployeeManager
    {
        readonly IEmployeeContext employeeContext;
        public EmployeeManager(IEmployeeContext _employee)
        {
            employeeContext = _employee;
        }
        public Employee IsValidEmployee(string UserId)
        {
            return employeeContext.IsValidEmployee(UserId);
        }

        public int Add(Employee employee)
        {
            return employeeContext.Add(employee);
        }
        public int Block(Guid UniqueId)
        {
            return employeeContext.Block(UniqueId);
        }
        public int UnBlock(Guid UniqueId)
        {
            return employeeContext.UnBlock(UniqueId);
        }
        public int Delete(Guid employeeId)
        {
            return employeeContext.Delete(employeeId);
        }
        public List<Employee> Get(string Name, int PageSize, int PageIndex, out int Count)
        {
            return employeeContext.Get(Name, PageSize,PageIndex,out Count);
        }
        public int Update(Employee employee)
        {
            return employeeContext.Update(employee);
        }
        public List<MappingData> GetMapping()
        {
            return employeeContext.GetMapping();
        }
        public int Add(Employee employee, out Guid UserId)
        {
            return employeeContext.Add(employee, out UserId);
        }
    }
}

using EaseLogMeIn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaseLogMeIn.DatabaseLayer
{

    public interface IEmployeeContext
    {
        int Add(Employee employee);
        int Add(Employee employee, out Guid UserId);
        int Update(Employee employee);
        int Delete(Guid employeeId);
        List<Employee> Get(string Name, int PageSize, int PageIndex, out int Count);
        int Block(Guid UniqueId);
        int UnBlock(Guid UniqueId);
        List<MappingData> GetMapping();
        //Desktop API
        Employee IsValidEmployee(string UserId);
    }
    public class EmployeeContext : IEmployeeContext
    {
        readonly EaseLogMeInDbContext easeLogMeIn = new EaseLogMeInDbContext();
        public int Add(Employee employee, out Guid UserId)
        {
            return AddEmployee(employee, out UserId);
        }
        public int Add(Employee employee)
        {
            return AddEmployee(employee, out _);
        }
        public int Delete(Guid employeeId)
        {
            var temp = easeLogMeIn.Employee.FirstOrDefault(e => e.UniqueId == employeeId);
            if (temp != null)
            {
                temp.IsDeleted = true;
                temp.IsActive = false;
                temp.DeleteDate = DateTime.Now.IST();
                temp.DeletedBy = AuthorizedUser.UserId;
                return easeLogMeIn.SaveChanges();
            }
            return 0;
        }
        public List<Employee> Get(string Name, int PageSize, int PageIndex, out int Count)
        {
            if (string.IsNullOrEmpty(Name))
            {
                return Search(PageSize, PageIndex, out Count);
            }
            return Search(Name, PageSize, PageIndex, out Count);
        }
        public int Update(Employee employee)
        {
            var temp = easeLogMeIn.Employee.FirstOrDefault(e => e.UniqueId == employee.UniqueId);
            if (temp != null)
            {
                temp.UpdateDate = DateTime.Now.IST();
                temp.UpdatedBy = AuthorizedUser.UserId;
                temp.Name = employee.Name;
                temp.UserId = employee.UserId;
                temp.IsNonADUser = employee.IsNonADUser;
                temp.MACId = employee.MACId;
                return easeLogMeIn.SaveChanges();
            }
            return 0;
        }
        public int UnBlock(Guid employeeId)
        {
            var temp = easeLogMeIn.Employee.FirstOrDefault(e => e.UniqueId == employeeId);
            if (temp != null)
            {
                temp.UpdateDate = DateTime.Now.IST();
                temp.UpdatedBy = AuthorizedUser.UserId;
                temp.IsDeleted = false;
                temp.IsActive = true;
                return easeLogMeIn.SaveChanges();
            }
            return 0;
        }
        public int Block(Guid employeeId)
        {
            var temp = easeLogMeIn.Employee.FirstOrDefault(e => e.UniqueId == employeeId);
            if (temp != null)
            {
                temp.UpdateDate = DateTime.Now.IST();
                temp.UpdatedBy = AuthorizedUser.UserId;
                temp.IsActive = false;
                return easeLogMeIn.SaveChanges();
            }
            return 0;
        }
        public List<MappingData> GetMapping()
        {
            List<MappingData> mappings = new List<MappingData>();
            var data = (from e in easeLogMeIn.Employee
                        where e.IsActive
                        select new
                        {
                            e.UniqueId,
                            e.Name
                        }).OrderBy(o => o.Name).ToList();

            for (int q = 0; q < data.Count; q++)
            {
                mappings.Add(new MappingData { Name = data[q].Name, UniqueId = data[q].UniqueId });
            }
            return mappings;
        }
        private List<Employee> Search(string Name, int PageSize, int PageIndex, out int Count)
        {
            int record = (PageSize * PageIndex);
            Count = easeLogMeIn.Employee.Where(w => w.IsActive && w.Name.Contains(Name)).Count();
            return easeLogMeIn.Employee.Where(w => w.IsActive && w.Name.Contains(Name))
                .OrderBy(w => w.Name)
                .Skip(record)
                .Take(PageSize).ToList();
        }
        private List<Employee> Search(int PageSize, int PageIndex, out int Count)
        {
            int record = (PageSize * PageIndex);
            Count = easeLogMeIn.Employee.Where(w => w.IsActive).Count();
            return easeLogMeIn.Employee.Where(w => w.IsActive)
                .OrderBy(w => w.Name)
                .Skip(record)
                .Take(PageSize).ToList();
        }
        private int AddEmployee(Employee employee, out Guid UserId)
        {
            var temp = easeLogMeIn.Employee.FirstOrDefault(e => e.UserId == employee.UserId);
            if (temp == null)
            {
                employee.UniqueId = Guid.NewGuid();
                employee.CreatedBy = AuthorizedUser.UserId;
                temp = easeLogMeIn.Employee.Add(employee);
                UserId = temp.UniqueId;
                return easeLogMeIn.SaveChanges();
            }
            UserId = temp.UniqueId;
            return 0;
        }

        // Desktop API
        public Employee IsValidEmployee(string UserId)
        {
            Employee emp = easeLogMeIn.Employee.FirstOrDefault(w => w.UserId == UserId && w.IsActive);
            if (emp != null)
            {

            }
            return emp;
        }

    }
}

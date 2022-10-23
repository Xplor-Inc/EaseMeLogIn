using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EaseLogMeIn.Models
{
    public class EmployeeModel
    {
        public List<MappingData> Websites { get; set; }
        public List<Employee> Employees { get; set; }
        public Employee Employee { get; set; }
        public Paging Paging { get; set; }
    }
}
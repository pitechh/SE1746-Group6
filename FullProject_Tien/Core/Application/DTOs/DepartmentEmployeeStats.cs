
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Core.Application.DTOs
{
    public class DepartmentEmployeeStats
    {
        public int TotalDepartments { get; set; }
        public Dictionary<string, int> EmployeesPerDepartment { get; set; }
    }
}
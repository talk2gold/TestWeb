using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeWeb.Models
{
    //currently we are not going to use this
    public class Employee
    {
        public int EmpNo { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; } 
        public DateTime JoinDate { get; set; } 
        public decimal Salary {get; set; } 
        public int MgrNo{ get; set; } 
        public int DeptID {get; set; }
        
    }


    // list employess
    public class EmployeeVM
    {
        public int EmpNo { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public DateTime JoinDate { get; set; }
        public decimal Salary { get; set; }
        public string MgrName { get; set; } = string.Empty;
        public string DeptName { get; set; } = string.Empty;
    }

    public class EmployeeUpsertVM   //Update /Insert
    {
        public int EmpNo { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public DateTime JoinDate { get; set; }
        public decimal Salary { get; set; }
        public int MgrNo { get; set; }
        public int DeptID { get; set; }

        public IEnumerable<SelectListItem> Depts { get; set; }
        public IEnumerable<SelectListItem> Managers { get; set; }

     
    }

    public class Departement
    {
        public int Id { get; set; }
        public string DeptName { get; set; } = string.Empty;
        public int MgrNo {get;set; }
    }
    public class Manager
    {
        public int MrgId { get; set; }
        public string MgrName { get; set; } = string.Empty;
    }

}
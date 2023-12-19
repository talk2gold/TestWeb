using Dapper;
using EmployeeWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace EmployeeWeb.Controllers
{
    public class EmployeeController : Controller
    {
        private IConfiguration _config;
        private IDbConnection _db;
        public EmployeeController(IConfiguration config)
        {
            _config = config;
            _db = new OracleConnection(_config.GetConnectionString("default"));
        }
        public IActionResult EmployeeList() 
        {
            //List of Employees
            //FirstName,LastName,Birth Date,DeptName,Manager Name, Join Date, Salary ( hidden : Empno, DeptId, MgrId)
            string sql = @"           
                            Select
                              Emp.Employee_id EmpNo, Emp.First_Name FirstName, Emp.Last_Name LastName
                            , Emp.Salary Salary, Emp.Hire_Date JoinDate
                            , Dept.Department_Name DeptName
                            , mgr.First_Name || ', ' || mgr.Last_Name MgrName
                            From Employees emp
                                 left join Departments Dept on Dept.Department_ID = Emp.Department_id
                                 left join Employees mgr on mgr.Employee_id = emp.Manager_ID  
                          ";

            IEnumerable<EmployeeVM> emplist = _db.Query<EmployeeVM>(sql);
            return View(emplist); 
        }
        public ActionResult Create()
        {
            EmployeeUpsertVM empUpsert = new();  //emptry emplyee structure

            string sql;
            //getting department
            sql = "select DEPARTMENT_ID Id, DEPARTMENT_NAME DeptName, MANAGER_ID MgrNo from departments order by 2";
            IEnumerable<Departement> deptList = _db.Query<Departement>(sql);
            empUpsert.Depts = deptList.Select(a => new SelectListItem
            {
                Text = a.DeptName,
                Value = a.Id.ToString(),
            });



            //get manager
            sql = @"Select
                          mgr.Employee_id MrgId, mgr.First_Name || ', ' || mgr.Last_Name MgrName
                     From Employees mgr order by 2
                   ";
            IEnumerable<Manager> emplist = _db.Query<Manager>(sql);
            empUpsert.Managers = emplist.Select(a => new SelectListItem
            {
                Text = a.MgrName,
                Value = a.MrgId.ToString(),
            });

            return View(empUpsert);
        }
        [HttpPost]
        public ActionResult Create(EmployeeUpsertVM empUpsert)
        {
            int empno = _db.Query<int>("select max(Employee_id) +1 empno from Employees");

            string sql = "Insert Into Employee(employee_id) values("");
                return View(empUpsert);
        }

        public ActionResult Edit(EmployeeUpsertVM empUpsert)
        {


            string sql;
            //getting department
            sql = "select DEPARTMENT_ID Id, DEPARTMENT_NAME DeptName, MANAGER_ID MgrNo from departments order by 2";
            IEnumerable<Departement> deptList = _db.Query<Departement>(sql);
            empUpsert.Depts = deptList.Select(a => new SelectListItem
            {
                Text = a.DeptName,
                Value = a.Id.ToString(),
                Selected= a.Id.Equals(empUpsert.DeptID)
            });


            //get manager
            sql = @"Select
                          mgr.Employee_id MrgId, mgr.First_Name || ', ' || mgr.Last_Name MgrName
                     From Employees mgr order by 2
                   ";
            IEnumerable<Manager> emplist = _db.Query<Manager>(sql);
            empUpsert.Managers = emplist.Select(a => new SelectListItem
            {
                Text = a.MgrName,
                Value = a.MrgId.ToString(),
            });

            return View(empUpsert);
        }
    }
}

using Dapper;
using EmployeeWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace EmployeeWeb.Controllers
{

    /*
     *  name = ""    ----Null
     *  newName = getOracleString(name);
     * 
     */
    public static class Utility
    {
        public static string getOracleString(string argString)
        {
            string ret = "null";
            if (argString is null )
            {
                ret = "null";
                
            }else if (argString =="")
            {
                ret = "null";
            }
            else if (argString.ToUpper() == "NULL")
            {
                ret = "null";
            }
            else            
            {
                ret = argString;
            }
            return ret;
        }
    }
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
            //string sql = @"           
            //                Select
            //                  Emp.Employee_id EmpNo, Emp.First_Name FirstName, Emp.Last_Name LastName
            //                , Emp.Salary Salary, Emp.Hire_Date JoinDate
            //                , Dept.Department_Name DeptName
            //                , mgr.First_Name || ', ' || mgr.Last_Name MgrName
            //                From emp emp
            //                     left join Departments Dept on Dept.Department_ID = Emp.Department_id
            //                     left join Employees mgr on mgr.Employee_id = emp.Manager_ID  
            //                order by 2
            //              ";

            string sql = "select * from vm_EmpList order by 2";
            IEnumerable<EmployeeVM> emplist = _db.Query<EmployeeVM>(sql);
            return View(emplist); 
        }


        public IActionResult EmployeeDataTable()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetEmployeeData()
        {

            string sql = "select * from vm_EmpList order by 2";
            IEnumerable<EmployeeVM> emplist = _db.Query<EmployeeVM>(sql);
            return Json( new { data = emplist });
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
                     From emp mgr order by 2
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

            if (!ModelState.IsValid)
            {
                return View(empUpsert);
            }
             

            string sql="";
            sql = "select max(Employee_id) +1 empno from Emp";
            int empno = _db.QuerySingle<int>(sql);

            sql = @"
                  Insert Into Emp ( FIRST_NAME, LAST_NAME, HIRE_DATE, SALARY, DEPARTMENT_ID, MANAGER_ID)
                  Values('" + empUpsert.FirstName + "', '" + empUpsert.LastName + "', TO_DATE('" + String.Format("{0:yyyy-MM-dd}", empUpsert.JoinDate)  + "','yyyy-mm-dd'), " + empUpsert.Salary + "," + empUpsert.DeptID + ", " + empUpsert.MgrNo + ")";
            int countNr = _db.Execute(sql);
            if (countNr > 0) 
            {
                return RedirectToAction( "EmployeeList", "Employee");
            }
            ModelState.AddModelError(string.Empty, "Database Insertion Error");
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
        
        public IActionResult AddEmployee()
        {
            EmployeeUpsertVM newEmp = new();
            string sql;
       



            //get manager
            sql = @"Select
                          mgr.Employee_id MrgId, mgr.First_Name || ', ' || mgr.Last_Name MgrName
                     From Employees mgr order by 2
                   ";
            IEnumerable<Manager> emplist = _db.Query<Manager>(sql);
            newEmp.Managers = emplist.Select(a => new SelectListItem
            {
                Text = a.MgrName,
                Value = a.MrgId.ToString(),
            });


            return View(newEmp);
            
        }

    }
}

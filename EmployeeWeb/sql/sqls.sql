Select
Emp.Employee_id EmpNo, Emp.Fname_Name FirstName, Emp.Last_Name LastName
, Emp.Salary Salary, Emp.Hire_Date JoinDate
, Dept.DeptName
, Mgr.MgrName
From Employees emp
     left join Departments Dept on Dept.Department_ID = Emp.Department_id
     left join (select Employee_id MgrNo
                     , First_Name || ', ' || Last_Name MgrName 
                  from employees) mgr on mgr.MgrNo = Emp.MANAGER_ID
;


user id=hr; password =hr; 
data source= (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = orcl)))
"
Select
  Emp.Employee_id EmpNo, Emp.First_Name FirstName, Emp.Last_Name LastName
, Emp.Salary Salary, Emp.Hire_Date JoinDate
, Dept.DEPARTMENT_NAME DeptName
, mgr.First_Name || ', ' || mgr.Last_Name MgrName
From Employees emp
     left join Departments Dept on Dept.Department_ID = Emp.Department_id
     left join Employees mgr on mgr.Employee_id = emp.MANAGER_ID ;
"                 
                  
"select DEPARTMENT_ID, DEPARTMENT_NAME, MANAGER_ID from departments;"



select DEPARTMENT_ID, DEPARTMENT_NAME, MANAGER_ID from departments;
select * from employees;
select Employee_id MgrNo, First_Name || ', ' || Last_Name MgrName from employees order by 2

Select
  Emp.Employee_id EmpNo, Emp.First_Name FirstName, Emp.Last_Name LastName
, Emp.Salary Salary, Emp.Hire_Date JoinDate
, Dept.DEPARTMENT_NAME DeptName
, mgr.First_Name || ', ' || mgr.Last_Name MgrName
From Employees emp
     left join Departments Dept on Dept.Department_ID = Emp.Department_id
     left join Employees mgr on mgr.Employee_id = emp.MANAGER_ID ;
     
Select
Emp.Employee_id EmpNo, Emp.First_Name FirstName, Emp.Last_Name LastName
, Emp.Salary Salary, Emp.Hire_Date JoinDate
, Dept.DEPARTMENT_NAME DeptName
, Mgr.MgrName
From Employees emp
     left join Departments Dept on Dept.Department_ID = Emp.Department_id
     left join (select Employee_id MgrNo
                     , First_Name || ', ' || Last_Name MgrName 
                  from employees) mgr on mgr.MgrNo = Emp.MANAGER_ID     
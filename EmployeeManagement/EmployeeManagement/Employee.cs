using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement
{
	internal class Employee
	{
		private int empId;

		public int EmpId
		{
			get { return empId; }
			set { empId = value; }
		}

		//empName, salary, department, type
		private string empName;

		public string EmpName
		{
			get { return empName; }
			set { empName = value; }
		}

		private double salary;

		public double Salary
		{
			get { return salary; }
			set { salary = value; }
		}

		private string department;

		public string Department
		{
			get { return department; }
			set { department = value; }
		}

		private string type;

		public string Type
		{
			get { return type; }
			set { type = value; }
		}

        public Employee(int id, string name, string dept, double salary, string type)
        {
            EmpId = id;
			EmpName = name;
			Department = dept;
			Salary = salary;
			Type = type;
        }


    }

}

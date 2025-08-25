using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EmployeeManagement
{
	public class Employee
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

		private double annualIncome;

		public double AnnualIncome
		{
			get { return annualIncome; }
			set { annualIncome = value; }
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

        public Employee(int id, string name, double income, string dept,string type)
        {
            EmpId = id;
			EmpName = name;
			Department = dept;
			AnnualIncome = income;
			Type = type;
        }

		public Employee() { }



    }

}

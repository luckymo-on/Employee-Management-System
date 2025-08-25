using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement
{
    public class SalaryProcessing
    {

        public static double calculateSalary(List<Employee> employees)
        {

            double salary = 0;
            Console.WriteLine("Enter Employee id");
            int id = int.Parse(Console.ReadLine());
            var emp = employees.Find(e => e.EmpId == id);
            if (emp == null) throw new KeyNotFoundException("Employee not found");
            if (emp.Type == "Permanent")
            {
                double BasicPay = emp.AnnualIncome / 12;
                double Allowance = (BasicPay * 20) / 100;
                double Deductions = (BasicPay * 10) / 100;

                salary = BasicPay + Allowance - Deductions;
                PayrollService.AddPayroll(emp.EmpId, emp.EmpName, emp.Department, emp.Type, BasicPay, Allowance, Deductions, salary);

            }
            else
            {
                Console.WriteLine("Enter the hours worked");
                double hours = double.Parse(Console.ReadLine());

                if (hours < 0) throw new ArgumentOutOfRangeException("Hours worked cannot be negative");

                double HourlyRate = 500.00;
                salary = hours * HourlyRate;


                PayrollService.AddPayroll(emp.EmpId, emp.EmpName, emp.Department, emp.Type, hours, HourlyRate, salary);
            }

            if (salary < 0) throw new InvalidOperationException("Salary cannot be negative");

            return salary;



        }


    }
}
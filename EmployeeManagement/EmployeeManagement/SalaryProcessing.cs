using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement
{
    internal class SalaryProcessing
    {

        public static double calculateSalary(List<Employee> employees)
        {
            double salary = 0;
            Console.WriteLine("Enter Employee id");
            int id = int.Parse(Console.ReadLine());
            var emp = employees.Find(e=>e.EmpId == id);
            if(emp.Type == "Permanent")
            {
                double BasicPay = emp.AnnualIncome / 12;
                double Allowance = (BasicPay * 20) / 100;
                double Deductions = (BasicPay * 10) / 100;

                salary = BasicPay + Allowance - Deductions;

            }
            else
            {
                Console.WriteLine("Enter the hours worked");
                double hours = double.Parse(Console.ReadLine());
                double HourlyRate = 500.00;
                salary = hours * HourlyRate;
            }
                return salary;
        }


    }
}

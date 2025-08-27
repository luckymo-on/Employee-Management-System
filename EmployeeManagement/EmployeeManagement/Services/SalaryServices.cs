using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Services
{
    public class SalaryServices
    {

        //Calculating Salaries
        private const double AllowancePercentage = 0.20;
        private const double DeductionPercentage = 0.10;
        private const double HourlyRate = 500.00;


        private static List<Employee> employees = new List<Employee>();
        public static void CalculateeSalaries()
        {
            try
            {
                employees = EmployeeServices.FetchEmployees();

                int count = 0;

                foreach (var emp in employees)
                {
                    // check if salary already generated
                    if (PayRollServices.Exists(emp.EmpId))
                    {
                        continue;
                    }

                    double salary = 0;

                    //permanent employee
                    if (emp.Type == "Permanent")
                    {
                        double basicPay = Math.Round(emp.AnnualIncome / 12, 2);
                        double allowance = Math.Round(basicPay * AllowancePercentage, 2);
                        double deductions = Math.Round(basicPay * DeductionPercentage, 2);

                        salary = basicPay + allowance - deductions;

                        PayRollServices.AddPayroll(emp.EmpId, emp.EmpName, emp.Department, emp.Type,
                                       basicPay, allowance, deductions, salary);
                    }
                    else // Contract employee
                    {
                        Console.WriteLine("Enter the hours worked");
                        double hours = double.Parse(Console.ReadLine());
                        Math.Round(hours, 2);
                        if (hours < 0)
                            throw new ArgumentOutOfRangeException("Hours worked cannot be negative");

                        salary = Math.Round(hours * HourlyRate, 2);

                        PayRollServices.AddPayroll(emp.EmpId, emp.EmpName, emp.Department, emp.Type, hours, HourlyRate, salary
                        );
                    }

                    count++;

                    if (salary < 0)
                        throw new InvalidOperationException("Salary cannot be negative");
                }
                if (count == 0)
                {
                    throw new Exception("Salary already generated for all employees");
                }

                Console.WriteLine("Salary generation completed.\nCan be viewed in payroll");
            }
            catch (Exception ex)
            {
                Ui.PrintError($"Error while generating salaries: {ex.Message}");
            }
        }

    }
}

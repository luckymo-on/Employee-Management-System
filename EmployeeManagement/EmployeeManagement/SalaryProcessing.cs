
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
            try
            {
                double salary = 0;
                Console.WriteLine("Enter Employee id");
                int id = int.Parse(Console.ReadLine());
                var emp = employees.Find(e => e.EmpId == id);
                if (emp == null) throw new KeyNotFoundException("Employee not found");
                if (emp.Type == "Permanent")
                {
                    double BasicPay = Math.Round(emp.AnnualIncome / 12, 2);
                    double Allowance = Math.Round((BasicPay * 20) / 100, 2);
                    double Deductions = Math.Round((BasicPay * 10) / 100, 2);

                    salary = BasicPay + Allowance - Deductions;
                    Console.WriteLine($"BasicPay: {BasicPay}");
                    Console.WriteLine($"Allowance: {Allowance}");
                    Console.WriteLine($"Deductions: {Deductions}");
                    Console.WriteLine($"Salary: {salary}");
                    PayrollService.AddPayroll(emp.EmpId, emp.EmpName, emp.Department, emp.Type, BasicPay, Allowance, Deductions, salary);

                }
                else
                {
                    Console.WriteLine("Enter the hours worked");
                    double hours = double.Parse(Console.ReadLine());
                    Math.Round(hours, 2);

                    if (hours < 0) throw new ArgumentOutOfRangeException("Hours worked cannot be negative");

                    double HourlyRate = Math.Round(500.00, 2);
                    salary = Math.Round(hours * HourlyRate, 2);

                    Console.WriteLine($"Hours Worked: {hours}");
                    Console.WriteLine($"Hourly Rates: {HourlyRate}");
                    Console.WriteLine($"Salary: {salary}");

                    PayrollService.AddPayroll(emp.EmpId, emp.EmpName, emp.Department, emp.Type, hours, HourlyRate, salary);
                }

                if (salary < 0) throw new InvalidOperationException("Salary cannot be negative");

                return salary;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;

            }

        }


    }
}



//using System;
//using System.Collections.Generic;

//namespace EmployeeManagement
//{
//    public static class SalaryProcessing
//    {
//        private const double AllowancePercentage = 0.20;
//        private const double DeductionPercentage = 0.10;
//        private const double DefaultHourlyRate = 500.00;

//        public static double CalculateSalary(Employee emp, double? hoursWorked = null)
//        {
//            if (emp == null)
//                throw new ArgumentNullException(nameof(emp), "Employee cannot be null");

//            if (emp.Type.Equals("Permanent", StringComparison.OrdinalIgnoreCase))
//            {
//                return CalculatePermanentSalary(emp);
//            }
//            else if (emp.Type.Equals("Contract", StringComparison.OrdinalIgnoreCase))
//            {
//                if (!hoursWorked.HasValue)
//                    throw new ArgumentException("Hours worked is required for contract employees");

//                return CalculateContractSalary(emp, hoursWorked.Value);
//            }
//            else
//            {
//                throw new NotSupportedException($"Employee type '{emp.Type}' is not supported");
//            }
//        }

//        private static double CalculatePermanentSalary(Employee emp)
//        {
//            double basicPay = Math.Round(emp.AnnualIncome / 12, 2);
//            double allowance = Math.Round(basicPay * AllowancePercentage, 2);
//            double deductions = Math.Round(basicPay * DeductionPercentage, 2);
//            double salary = basicPay + allowance - deductions;

//            if (salary < 0) throw new InvalidOperationException("Calculated salary cannot be negative");

//            Console.WriteLine($"BasicPay: {basicPay}");
//            Console.WriteLine($"Allowance: {allowance}");
//            Console.WriteLine($"Deductions: {deductions}");
//            Console.WriteLine($"Salary: {salary}");

//            PayrollService.AddPayroll(emp.EmpId, emp.EmpName, emp.Department, emp.Type, basicPay, allowance, deductions, salary);

//            return salary;
//        }

//        private static double CalculateContractSalary(Employee emp, double hours)
//        {
//            if (hours < 0)
//                throw new ArgumentOutOfRangeException(nameof(hours), "Hours worked cannot be negative");

//            double hourlyRate = DefaultHourlyRate;
//            double salary = Math.Round(hours * hourlyRate, 2);

//            if (salary < 0) throw new InvalidOperationException("Calculated salary cannot be negative");

//            Console.WriteLine($"Hours Worked: {hours}");
//            Console.WriteLine($"Hourly Rate: {hourlyRate}");
//            Console.WriteLine($"Salary: {salary}");

//            PayrollService.AddPayroll(emp.EmpId, emp.EmpName, emp.Department, emp.Type, hours, hourlyRate, salary);

//            return salary;
//        }
//    }
//}

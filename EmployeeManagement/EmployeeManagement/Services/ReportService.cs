using EmployeeManagement.Models;
using EmployeeManagement.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.Services
{
    public static class ReportService
    {
        // Filter employees by name (case-insensitive contains)
        private static List<Employee> employees = new List<Employee>();
        public static void EmployeeByName(string namePart)
        {
            employees = EmployeeServices.FetchEmployees();
            var results = employees
                .Where(e => e.EmpName.Contains(namePart, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (results.Count == 0)
            {
                Ui.PrintError($"No employees found with name containing '{namePart}'.");
                return;
            }

            Console.WriteLine($"Employees with name containing '{namePart}':");

            Console.WriteLine("+--------+------------------+------------+-----------+---------------+");
            Console.WriteLine("| EmpId  | EmpName           | Department | Type      | AnnualIncome  |");
            Console.WriteLine("+--------+------------------+------------+-----------+---------------+");

            // Print employee rows
            foreach (var emp in results)
            {
                Console.WriteLine("| {0,-6} | {1,-16} | {2,-10} | {3,-9} | {4,13:N2} |",
                    emp.EmpId, emp.EmpName, emp.Department, emp.Type, emp.AnnualIncome);
            }

            // Print bottom border
            Console.WriteLine("+--------+------------------+------------+-----------+---------------+");

        }

        // Filter employees by department
        public static void EmployeeByDepartment(string dept)
        {
            employees = EmployeeServices.FetchEmployees();
            var results = employees
                .Where(e => e.Department.Equals(dept, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (results.Count == 0)
            {
                Ui.PrintError($"No employees found in department '{dept}'.");
                return;
            }

            Console.WriteLine($"Employees in department '{dept}':");
            Console.WriteLine("+--------+------------------+------------+-----------+---------------+");
            Console.WriteLine("| EmpId  | EmpName           | Department | Type      | AnnualIncome  |");
            Console.WriteLine("+--------+------------------+------------+-----------+---------------+");

            // Print employee rows
            foreach (var emp in results)
            {

                Console.WriteLine("| {0,-6} | {1,-16} | {2,-10} | {3,-9} | {4,13:N2} |",
                   emp.EmpId, emp.EmpName, emp.Department, emp.Type, emp.AnnualIncome);
            }
            // Print bottom border
            Console.WriteLine("+--------+------------------+------------+-----------+---------------+");

        }

        // Generate salary summary (latest payroll entry for given employee)
        public static void PaySlip(int empId)
        {
            var payrolls = FileRepo.Fetch()
                .Where(p => p.EmployeeId == empId)
                .OrderByDescending(p => p.PaymentDate)
                .Take(3) // last 3 months
                .ToList();

            if (payrolls.Count == 0)
            {
                Console.WriteLine($"No payroll records found for EmployeeId {empId}.");
                return;
            }

            Console.WriteLine("\nAvailable Payslips (Last 3 Months):");
            Console.WriteLine();
            Console.WriteLine("+----+----------------+------------+");
            Console.WriteLine("| #  | Payment Month  | Salary     |");
            Console.WriteLine("+----+----------------+------------+");
            for (int i = 0; i < payrolls.Count; i++)
            {
                var p = payrolls[i];
                Console.WriteLine("| {0,-2} | {1,-14} | {2,10:N2} |", i + 1, p.PaymentDate.ToString("MMMM yyyy"), p.Salary);

            }
            Console.WriteLine("+----+----------------+------------+");

            Console.Write("\nSelect a month (1 - {0}): ", payrolls.Count);
            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > payrolls.Count)
            {
                Ui.PrintError("Invalid choice. Exiting payslip generation.");
                return;
            }

            var selected = payrolls[choice - 1];

            //Console.Clear();
            int width = 45; // fixed width for border
            string border = new string('=', width);
            string separator = new string('-', width);

            Console.WriteLine(border);
            Console.WriteLine("                 PAYSLIP                 ");
            Console.WriteLine(border);
            Console.WriteLine($"| {"Employee ID",-15} : {selected.EmployeeId,20} |");
            Console.WriteLine($"| {"Name",-15} : {selected.EmpName,20} |");
            Console.WriteLine($"| {"Department",-15} : {selected.Department,20} |");
            Console.WriteLine($"| {"Type",-15} : {selected.Type,20} |");
            Console.WriteLine(separator);
            if (selected.Type == "Permanent")
            {
                Console.WriteLine($"| {"Basic Pay",-15} : {selected.BasicPay,20:F2} |");
                Console.WriteLine($"| {"Allowance",-15} : {selected.Allowance,20:F2} |");
                Console.WriteLine($"| {"Deductions",-15} : {selected.Deductions,20:F2} |");
            }

            else if (selected.Type == "Contract")
            {
                Console.WriteLine($"| {"Hours Worked",-15} : {selected.Hours,20} |");
                Console.WriteLine($"| {"Hourly Rate",-15} : {selected.HourlyRate,20:F2} |");
            }

            Console.WriteLine(separator);
            Console.WriteLine($"| {"Net Salary",-15} : {selected.Salary,20:F2} |");
            Console.WriteLine($"| {"Payment Date",-15} : {selected.PaymentDate,20:dd-MMM-yyyy} |");
            Console.WriteLine(border);
        }

        // Find Employees by Type
        public static void EmployeeByType()
        {
            var payrolls = FileRepo.Fetch();

            if (payrolls.Count == 0)
            {
                Ui.PrintError("No payroll records found.");
                return;
            }

            Console.WriteLine("===== Permanent Employees =====");
            var permanent = payrolls
                .Where(p => p.Type.Equals("Permanent", StringComparison.OrdinalIgnoreCase))
                .GroupBy(p => p.EmployeeId) // one record per employee (latest)
                .Select(g => g.OrderByDescending(p => p.PaymentDate).First());

            if (!permanent.Any())
            {
                Ui.PrintError("No permanent employees found.");
            }
            
                //    Console.WriteLine("+--------+------------------+------------+-----------+---------------+");
                //    Console.WriteLine("| EmpName  | BasicPay           | Allowamce | Deduction    | Salary |");
                //    Console.WriteLine("+--------+------------------+------------+-----------+---------------+");
                //    foreach (var p in permanent)
                //    {
                //        Console.WriteLine($"{p.EmpName} | BasicPay: {p.BasicPay} | Allowance: {p.Allowance} | Deductions: {p.Deductions} | Salary: {p.Salary}");
                //        Console.WriteLine("| {0,-6} | {1,-16} | {2,-10} | {3,-9} | {4,13:N2} |",
                //                p.EmpName, p.BasicPay.Value.ToString("N2"), p.Allowance.Value.ToString("N2"), p.Deductions.Value.ToString("N2"), p.Salary);

                //    }
                //}

                //Console.WriteLine("\n===== Contract Employees =====");
                //var contract = payrolls
                //    .Where(p => p.Type.Equals("Contract", StringComparison.OrdinalIgnoreCase))
                //    .GroupBy(p => p.EmployeeId)
                //    .Select(g => g.OrderByDescending(p => p.PaymentDate).First());

                //if (!contract.Any())
                //{
                //    Console.WriteLine("No contract employees found.");
                //}
                //else
                //{
                //    foreach (var p in contract)
                //    {
                //        Console.WriteLine($"{p.EmpName} | Hours: {p.Hours} | HourlyRate: {p.HourlyRate} | Salary: {p.Salary}");
                //    }
                //}
                else
                {
                    // Table header
                    Console.WriteLine("+------------+---------------+------------+------------+---------------+");
                    Console.WriteLine("| EmpName    | BasicPay      | Allowance  | Deduction  | Salary        |");
                    Console.WriteLine("+------------+---------------+------------+------------+---------------+");

                    // Table rows
                    foreach (var p in permanent)
                    {
                        Console.WriteLine("| {0,-10} | {1,13:N2} | {2,10:N2} | {3,10:N2} | {4,13:N2} |",
                            p.EmpName, p.BasicPay ?? 0, p.Allowance ?? 0, p.Deductions ?? 0, p.Salary);
                    }

                    Console.WriteLine("+------------+---------------+------------+------------+---------------+");
                }

                Console.WriteLine("\n===== Contract Employees =====");
                var contract = payrolls
                    .Where(p => p.Type.Equals("Contract", StringComparison.OrdinalIgnoreCase))
                    .GroupBy(p => p.EmployeeId)
                    .Select(g => g.OrderByDescending(p => p.PaymentDate).First());

                if (!contract.Any())
                {
                    Console.WriteLine("No contract employees found.");
                }
                else
                {
                    // Table header
                    Console.WriteLine("+------------+-------+------------+---------------+");
                    Console.WriteLine("| EmpName    | Hours | HourlyRate | Salary        |");
                    Console.WriteLine("+------------+-------+------------+---------------+");

                    // Table rows
                    foreach (var p in contract)
                    {
                        Console.WriteLine("| {0,-10} | {1,5} | {2,10:N2} | {3,13:N2} |",
                            p.EmpName, p.Hours, p.HourlyRate ?? 0, p.Salary);
                    }

                    Console.WriteLine("+------------+-------+------------+---------------+");
                }


            }
        }
}


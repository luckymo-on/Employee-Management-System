using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement
{
    public static class ReportService
    {
        // Filter employees by name (case-insensitive contains)
        public static void EmployeeByName(List<Employee> employees, string namePart)
        {
            var results = employees
                .Where(e => e.EmpName.Contains(namePart, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (results.Count == 0)
            {
                Console.WriteLine($"No employees found with name containing '{namePart}'.");
                return;
            }

            Console.WriteLine($"Employees with name containing '{namePart}':");
            foreach (var emp in results)
            {
                Console.WriteLine($"{emp.EmpId} - {emp.EmpName} - {emp.Department} - {emp.Type} - {emp.AnnualIncome}");
            }
        }

        // Filter employees by department
        public static void EmployeeByDepartment(List<Employee> employees, string dept)
        {
            var results = employees
                .Where(e => e.Department.Equals(dept, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (results.Count == 0)
            {
                Console.WriteLine($"No employees found in department '{dept}'.");
                return;
            }

            Console.WriteLine($"Employees in department '{dept}':");
            foreach (var emp in results)
            {
                Console.WriteLine($"{emp.EmpId} - {emp.EmpName} - {emp.Department} - {emp.Type} - {emp.AnnualIncome}");
            }
        }

        // Generate salary summary (latest payroll entry for given employee)
        public static void SalarySummaryByEmployee(int empId)
        {
            var payrolls = PayrollService.Fetch()
                .Where(p => p.EmployeeId == empId)
                .OrderByDescending(p => p.PaymentDate)
                .ToList();

            if (payrolls.Count == 0)
            {
                Console.WriteLine($"No payroll records found for EmployeeId {empId}.");
                return;
            }

            var latest = payrolls.First();
            Console.WriteLine("----- Payslip -----");
            Console.WriteLine($"EmployeeId : {latest.EmployeeId}");
            Console.WriteLine($"Name       : {latest.EmpName}");
            Console.WriteLine($"Department : {latest.Department}");
            Console.WriteLine($"Type       : {latest.Type}");
            Console.WriteLine($"Basic Pay  : {latest.BasicPay}");
            Console.WriteLine($"Allowance  : {latest.Allowance}");
            Console.WriteLine($"Deductions : {latest.Deductions}");
            Console.WriteLine($"Hours      : {latest.Hours}");
            Console.WriteLine($"HourlyRate : {latest.HourlyRate}");
            Console.WriteLine($"Salary     : {latest.Salary}");
            Console.WriteLine($"Date       : {latest.PaymentDate}");
            Console.WriteLine("-------------------");
        }

        // Find Employees by Type
        public static void EmployeeByType()
        {
            var payrolls = PayrollService.Fetch();

            if (payrolls.Count == 0)
            {
                Console.WriteLine("No payroll records found.");
                return;
            }

            Console.WriteLine("===== Permanent Employees =====");
            var permanent = payrolls
                .Where(p => p.Type.Equals("Permanent", StringComparison.OrdinalIgnoreCase))
                .GroupBy(p => p.EmployeeId) // one record per employee (latest)
                .Select(g => g.OrderByDescending(p => p.PaymentDate).First());

            if (!permanent.Any())
            {
                Console.WriteLine("No permanent employees found.");
            }
            else
            {
                foreach (var p in permanent)
                {
                    Console.WriteLine($"{p.EmpName} | BasicPay: {p.BasicPay} | Allowance: {p.Allowance} | Deductions: {p.Deductions} | Salary: {p.Salary}");
                }
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
                foreach (var p in contract)
                {
                    Console.WriteLine($"{p.EmpName} | Hours: {p.Hours} | HourlyRate: {p.HourlyRate} | Salary: {p.Salary}");
                }
            }
        }
    }
}


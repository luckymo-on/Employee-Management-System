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
        public static void EmployeeByName( string namePart)
        {
            employees = EmployeeServices.FetchEmployees();
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
        public static void EmployeeByDepartment( string dept)
        {
            employees = EmployeeServices.FetchEmployees();
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
            for (int i = 0; i < payrolls.Count; i++)
            {
                var p = payrolls[i];
                Console.WriteLine($"{i + 1}. {p.PaymentDate:MMMM yyyy} (Salary: {p.Salary})");
            }

            Console.Write("\nSelect a month (1 - {0}): ", payrolls.Count);
            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > payrolls.Count)
            {
                Console.WriteLine("Invalid choice. Exiting payslip generation.");
                return;
            }

            var selected = payrolls[choice - 1];

            Console.WriteLine("\n----- Payslip -----");
            Console.WriteLine($"EmployeeId : {selected.EmployeeId}");
            Console.WriteLine($"Name       : {selected.EmpName}");
            Console.WriteLine($"Department : {selected.Department}");
            Console.WriteLine($"Type       : {selected.Type}");
            Console.WriteLine($"Basic Pay  : {selected.BasicPay}");
            Console.WriteLine($"Allowance  : {selected.Allowance}");
            Console.WriteLine($"Deductions : {selected.Deductions}");

            if (selected.Type == "Contract")
            {
                Console.WriteLine($"Hours      : {selected.Hours}");
                Console.WriteLine($"HourlyRate : {selected.HourlyRate}");
            }

            Console.WriteLine($"Salary     : {selected.Salary}");
            Console.WriteLine($"Date       : {selected.PaymentDate:dd-MMM-yyyy}");
            Console.WriteLine("-------------------\n");
        }


        // Find Employees by Type
        public static void EmployeeByType(List<Employee> employees)
        {
            var payrolls = FileRepo.Fetch();
            var permanent = employees.Where(e => e.Type.Equals("Permanent", StringComparison.OrdinalIgnoreCase));
            var contract = employees.Where(e => e.Type.Equals("Contract", StringComparison.OrdinalIgnoreCase));

            Console.WriteLine("===== Permanent Employees =====");
            if (!permanent.Any())
            {
                Console.WriteLine("No permanent employees found.");
            }

            else
            {
                foreach (var emp in permanent)
                {
                    Console.WriteLine($"{emp.EmpId} - {emp.EmpName} - {emp.Department} - {emp.AnnualIncome}");
                }
            }       

            Console.WriteLine("\n===== Contract Employees =====");
            if (!contract.Any())
            {
                Console.WriteLine("No contract employees found.");
            }
            else
            {
                foreach (var emp in contract)
                {
                    Console.WriteLine($"{emp.EmpId} - {emp.EmpName} - {emp.Department} - {emp.AnnualIncome}");
                }
            }         
        }
    }
}


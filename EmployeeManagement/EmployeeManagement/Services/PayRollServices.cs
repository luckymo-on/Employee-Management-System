using EmployeeManagement.Models;
using EmployeeManagement.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Services
{
    public class PayRollServices
    {

        private const int id = 100;
        private static int payrollID = 0;

        public static List<Payroll> payroll = new List<Payroll>();

        
        //AddPayroll for Permanent employees
        public static void AddPayroll(int employeeID, string employeeName, string department, string type, double basicPay, double allowance, double deductions, double salary)
        {
            DateOnly date = DateOnly.FromDateTime(DateTime.Now);

            //Saving to list
            payroll = FileRepo.Fetch();

            //Checking for Duplicate payroll entries
            if (payroll.Any(p => p.EmployeeId == employeeID &&
            p.PaymentDate.Month == date.Month &&
            p.PaymentDate.Year == date.Year))
            {
                throw new InvalidOperationException($"Payroll entry already exists for the Employee {employeeName} on {date}");
            }

            if (payroll.Count == 0)
            {
                payrollID = id;
            }
            else
            {
                int max = payroll.Max(p => p.PayrollId);
                payrollID = max + 1;
            }

            //Adding to the list
            Payroll newPayroll = new Payroll(payrollID, employeeID, employeeName, department, type, basicPay, allowance, deductions, null, null, salary, date);
            payroll.Add(newPayroll);
            //Saving to the file
            FileRepo.SaveTOFile(newPayroll,payroll);
            DatabaseRepo.SaveToDb(newPayroll);

        }

        //AddPayroll for Contract employees
        public static void AddPayroll(int employeeID, string employeeName, string department, string type, double hours, double hourlyRate, double salary)
        {
            DateOnly date = DateOnly.FromDateTime(DateTime.Now);

            //Saving to List
            payroll = FileRepo.Fetch();

            //Checking for Duplicate payroll entries
            if (payroll.Any(p => p.EmployeeId == employeeID &&
            p.PaymentDate.Month == date.Month &&
            p.PaymentDate.Year == date.Year))
            {
                throw new InvalidOperationException($"Payroll entry already exists for the Employee {employeeName} on {date}");

            }

            if (payroll.Count == 0)
            {
                payrollID = id;
            }
            else
            {
                int max = payroll.Max(p => p.PayrollId);
                payrollID = max + 1;
            }

            //Adding to the list
            Payroll newPayroll = new Payroll(payrollID, employeeID, employeeName, department, type, null, null, null, hours, hourlyRate, salary, date);
            payroll.Add(newPayroll);
            //Saving to File
            FileRepo.SaveTOFile(newPayroll,payroll);
            DatabaseRepo.SaveToDb(newPayroll);
        }

        public static void display()
        {
            payroll = FileRepo.Fetch();
            if (payroll.Count == 0)
            {
                throw new InvalidOperationException("Payroll History is Empty");
            }
            else
            {
                Console.WriteLine("+-----------+------------+----------------+------------+-----------+------------+------------+------------+-----------+-----------+------------+----------------+");
                Console.WriteLine("| PayrollId | EmployeeId | EmpName        | Department | Type      | BasicPay   | Allowance  | Deductions | Hours     | HourlyRate| Salary     | PaymentDate    |");
                Console.WriteLine("+-----------+------------+----------------+------------+-----------+------------+------------+------------+-----------+-----------+------------+----------------+");

                foreach (Payroll p in payroll)
                {
                    Console.WriteLine("| {0,-9} | {1,-10} | {2,-14} | {3,-10} | {4,-9} | {5,10} | {6,10} | {7,10} | {8,9} | {9,9} | {10,10:N2} | {11,-14} |",
                        p.PayrollId,
                        p.EmployeeId,
                        p.EmpName,
                        p.Department,
                        p.Type,
                        p.BasicPay.HasValue ? p.BasicPay.Value.ToString("N2") : "N/A",
                        p.Allowance.HasValue ? p.Allowance.Value.ToString("N2") : "N/A",
                        p.Deductions.HasValue ? p.Deductions.Value.ToString("N2") : "N/A",
                        p.Hours.HasValue ? p.Hours.Value.ToString("N2") : "N/A",
                        p.HourlyRate.HasValue ? p.HourlyRate.Value.ToString("N2") : "N/A",
                        p.Salary,
                        p.PaymentDate.ToString("yyyy-MM-dd")
                    );
                }

                Console.WriteLine("+-----------+------------+----------------+------------+-----------+------------+------------+------------+-----------+-----------+------------+----------------+");
            }
        }

        //Checking for duplication
        public static bool Exists(int empId)
        {
            return payroll.Any(p => p.EmployeeId == empId);
        }
    }
}

using EmployeeManagement.UI;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{

    public class Payroll
    {
        public int PayrollId { get; set; }
        public int EmployeeId { get; set; }
        public string EmpName { get; set; }
        public string Department { get; set; }
        public string Type { get; set; }
        public double? BasicPay { get; set; }
        public double? Allowance { get; set; }
        public double? Deductions { get; set; }
        public double? Hours { get; set; }
        public double? HourlyRate { get; set; }
        public double Salary { get; set; }

        public DateOnly PaymentDate { get; set; }

        public Payroll()
        {

        }

        public Payroll(int payrollID, int employeeID, string employeeName, string department, string type, double? basicPay, double? allowance, double? deductions, double? hours, double? hourlyRate, double salary, DateOnly paymentDate)
        {
            PayrollId = payrollID;
            EmployeeId = employeeID;
            EmpName = employeeName;
            Department = department;
            Type = type;
            BasicPay = basicPay;
            Allowance = allowance;
            Deductions = deductions;
            Hours = hours;
            HourlyRate = hourlyRate;
            Salary = salary;
            PaymentDate = paymentDate;
        }


    }
}
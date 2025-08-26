using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmployeeManagement
{

    public class Payroll
    {
        public int PayrollId { get; set; }
        public int EmployeeId { get; set; }
        public String EmpName { get; set; }
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
    public static class PayrollService
    {
        private const int id = 100;
        private static int payrollID = 0;

        public static List<Payroll> payroll = new List<Payroll>();

        //AddPayroll for Permanent employees
        public static void AddPayroll(int employeeID, string employeeName, string department, string type, double basicPay, double allowance, double deductions, double salary)
        {
            DateOnly date = DateOnly.FromDateTime(DateTime.Now);

            //Saving to list
            payroll = Fetch();

            //Checking for Duplicate payroll entries
            if (payroll.Any(p => p.EmployeeId == employeeID && p.EmpName == employeeName))
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
            Save(newPayroll);

        }

        //AddPayroll for Contract employees
        public static void AddPayroll(int employeeID, string employeeName, string department, string type, double hours, double hourlyRate, double salary)
        {
            DateOnly date = DateOnly.FromDateTime(DateTime.Now);

            //Saving to List
            payroll = Fetch();

            //Checking for Duplicate payroll entries
            if (payroll.Any(p => p.EmployeeId == employeeID && p.EmpName == employeeName))
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
             Payroll newPayroll=new Payroll(payrollID, employeeID, employeeName, department, type, null, null, null, hours, hourlyRate, salary, date);
            payroll.Add(newPayroll);
            //Saving to File
            Save(newPayroll);
        }
        public static void display()
        {
            payroll = Fetch();
            if (payroll.Count == 0)
            {
                throw new InvalidOperationException("Payroll History is Empty");
            }
            else
            {
                foreach (Payroll p in payroll)
                {
                    Console.WriteLine($"PayrollId: {p.PayrollId}, EmployeeId: {p.EmployeeId}, EmpName: {p.EmpName}, Department: {p.Department}, Type: {p.Type}, BasicPay: {p.BasicPay}, Allowance: {p.Allowance}, Deductions: {p.Deductions}, Hours: {p.Hours}, HourlyRate: {p.HourlyRate}, Salary: {p.Salary}, PaymentDate: {p.PaymentDate}");

                }

            }
        }

        public static List<Payroll> Fetch()
        {
            List<Payroll> temp = new List<Payroll>();
            using (StreamReader sr = new StreamReader("../../../PayrollHistory.txt"))
            {
                string data;
                while ((data = sr.ReadLine()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(data))
                    {
                        Payroll record = JsonSerializer.Deserialize<Payroll>(data);
                        temp.Add(record);
                    }
                }
            }
            return temp;
        }
        public static void Save(Payroll newPayroll)
        {

            //Saving to File
            using (StreamWriter sw = new StreamWriter("../../../PayrollHistory.txt"))
            {
                foreach (var record in payroll)
                {
                    string data = JsonSerializer.Serialize(record, new JsonSerializerOptions { WriteIndented = false });
                    sw.WriteLine(data);
                }

            }

            //Saving to DB

            using (SqlConnection connection = Program.ConnectToDb())//Calling the function from program class
            {
                Console.WriteLine(connection.State);
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "INSERT INTO Payroll (PayrollId, EmployeeId, EmpName, Department, Type, BasicPay, Allowance, Deductions, Hours, HourlyRate, Salary, PaymentDate) VALUES (@PayrollId, @EmployeeId, @EmpName, @Department, @Type, @BasicPay, @Allowance, @Deductions, @Hours, @HourlyRate, @Salary, @PaymentDate)";
                cmd.Parameters.AddWithValue("@PayrollId", newPayroll.PayrollId);
                cmd.Parameters.AddWithValue("@EmployeeId", newPayroll.EmployeeId);
                cmd.Parameters.AddWithValue("@EmpName", newPayroll.EmpName);
                cmd.Parameters.AddWithValue("@Department", newPayroll.Department);
                cmd.Parameters.AddWithValue("@Type", newPayroll.Type);
                cmd.Parameters.AddWithValue("@BasicPay", newPayroll.BasicPay ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Allowance", newPayroll.Allowance ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Deductions", newPayroll.Deductions ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Hours", newPayroll.Hours ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@HourlyRate", newPayroll.HourlyRate ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Salary", newPayroll.Salary);
                cmd.Parameters.AddWithValue("@PaymentDate", newPayroll.PaymentDate.ToDateTime(TimeOnly.MinValue));

                cmd.ExecuteNonQuery();
            }
           

        }
    }
}
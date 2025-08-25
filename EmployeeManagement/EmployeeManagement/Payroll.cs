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

        public Payroll(int payrollID,int employeeID, string employeeName, string department, string type, double? basicPay, double? allowance, double? deductions,  double? hours, double? hourlyRate, double salary, DateOnly paymentDate)
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
            Type = type;
        }


    }
    public static class PayrollService
    {
        private const int id = 100;
        private static int payrollID = 0;
        public static List<Payroll> payroll = new List<Payroll>();

        public static void AddPayroll(int employeeID, string employeeName, string department, string type, double basicPay, double allowance, double deductions, double salary)
        {
            DateOnly date = DateOnly.FromDateTime(DateTime.Now);

            //Saving to list
            payroll = Fetch();
           
            if (payroll.Count == 0)
            {
                payrollID = id;
            }
            else
            {
                payrollID = payroll.Count + 1;
            }

            //Adding to the list
            payroll.Add(new Payroll(payrollID,employeeID, employeeName, department, type, basicPay, allowance, deductions, null, null, salary, date));

            //Saving to the file
            Save();
            
        }
        public static void AddPayroll(int employeeID, string employeeName, string department, string type, double hours, double hourlyRate, double salary)
        {
            DateOnly date = DateOnly.FromDateTime(DateTime.Now);

            //Saving to List
            payroll = Fetch();

            if (payroll.Count == 0)
            {
                payrollID = id;
            }
            else
            {
                payrollID = payroll.Count + 1;
            }
            //Adding to the list
            payroll.Add(new Payroll(payrollID,employeeID, employeeName, department, type,null,null,null, hours, hourlyRate, salary, date));

            //Saving to File
            Save();
        }
        public static void display()
        {
            payroll = Fetch();
            if(payroll.Count==0)
            {
                Console.WriteLine("Payroll History is Empty");
            }
            else
            {
                foreach(Payroll p in payroll)
                {
                    Console.WriteLine($"PayrollId: {p.PayrollId}, EmployeeId: {p.EmployeeId}, EmpName: {p.EmpName}, Department: {p.Department}, Type: {p.Type}, BasicPay: {p.BasicPay}, Allowance: {p.Allowance}, Deductions: {p.Deductions}, Hours: {p.Hours}, HourlyRate: {p.HourlyRate}, Salary: {p.Salary}, PaymentDate: {p.PaymentDate}");

                }
                
            }
        }

        public static List<Payroll> Fetch()
        {
            String data = null;
            using(StreamReader sr=new StreamReader("../../../PayrollHistory.txt"))
            {
                data = sr.ReadToEnd();
            }
            List<Payroll> temp = new List<Payroll>();
            if(!string.IsNullOrWhiteSpace(data))
            {
                temp = JsonSerializer.Deserialize<List<Payroll>>(data);
                
            }
            return temp;
        }
        public static void Save()
        {
            using (StreamWriter sw=new StreamWriter("../../../PayrollHistory.txt"))
            {
                string data=JsonSerializer.Serialize(payroll);
                sw.WriteLine(data);
            }
        }
    }
}
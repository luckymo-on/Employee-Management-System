using System.ComponentModel;
using System.Text.Json;

namespace EmployeeManagement
{
    internal class Program
    {

       public static List<Employee> employees = new List<Employee>();
        static void Main(string[] args)
        {

            DisplayAll();
            
            int choice = 0;
            while (choice != -1)
            {
                Console.WriteLine("1.Add employee\n2.Update employee Details\n3.View Employees\n4.Calculate Salary\n5.Exit\nEnter Your Choice :");
                choice = int.Parse(Console.ReadLine());
                if (choice == 1)
                {
                    //AddingNew Employee
                    AddEmployee();
                    Console.WriteLine("______________________________________");

                }
                else
                {
                    //Developing in process
                }
            }

        }



        public static void DisplayAll()
        {
            String json = "";
            using (StreamReader streamReader = new StreamReader("../../../db.txt"))
            {
                json = streamReader.ReadToEnd();
            }
            if (!string.IsNullOrWhiteSpace(json))
            {

                List<Employee> currentdetails = new List<Employee>();
                if (json != "")
                {
                    currentdetails = JsonSerializer.Deserialize<List<Employee>>(json);

                    employees = currentdetails;
                }
            }



            Console.WriteLine("Welcome To Employee Management System");
            Console.WriteLine("______________________________________");


            if (employees.Count > 0)
            {
                Console.WriteLine("Current Employees Are :");
                foreach (var item in employees)
                {
                    Console.WriteLine($"{item.EmpId} - {item.EmpName} - {item.Department} - {item.Type} - {item.AnnualIncome}");
                }
                Console.WriteLine("______________________________________");
            }
        }



        public static void AddEmployee()
        {
            Console.WriteLine("Enter the employee name :");
            string name = Console.ReadLine();
            Console.WriteLine("Enter the employee department :");
            string dept = Console.ReadLine();
            Console.WriteLine("Enter the employee type \np.Permanant\nc.Contract:");
            string ch = Console.ReadLine().ToLower();
            string type = null;
            if (ch == "p")
            {
                type = "Permanent";
            }
            else if (ch == "c")
            {
                type = "Contract";
            }
            else
            {
                Console.WriteLine("Invalid input");
            }
            Console.WriteLine("Enter the employee salary :");
            double income = double.Parse(Console.ReadLine());

            employees.Add(new Employee(employees.Count + 1, name, income, dept,type));

            using (StreamWriter sr = new StreamWriter("../../../db.txt"))
            {
                string data = JsonSerializer.Serialize(employees);
                sr.WriteLine(data);
            }
        }
    }
}

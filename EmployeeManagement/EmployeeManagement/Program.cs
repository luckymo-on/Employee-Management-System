using System.ComponentModel;
using System.Text.Json;

namespace EmployeeManagement
{
    internal class Program
    {

       public static List<Employee> employees = new List<Employee>();
        static void Main(string[] args)
        {

            Console.WriteLine("Welcome To Employee Management System");
            Console.WriteLine("______________________________________");

            DisplayAll();

            Console.WriteLine("______________________________________");

            int choice = 0;
            while (choice != -1)
            {
                Console.WriteLine("1.Add employee\n2.Update employee Details\n3.View Employees\n4.Calculate Salary\n5.Exit\nEnter Your Choice :");
                choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        // Adding New Employee
                        Console.WriteLine("______________________________________");
                        AddEmployee();
                        Console.WriteLine("______________________________________");
                        break;

                    case 2:
                        // Updating Employee
                        Console.WriteLine("______________________________________");
                        UpdateEmployee();
                        Console.WriteLine("______________________________________");
                        break;
                    case 3:
                        Console.WriteLine("______________________________________");
                        DisplayAll();
                        Console.WriteLine("______________________________________");
                        break;
                        case 4:
                        Console.WriteLine("______________________________________");
                        double salary = SalaryProcessing.calculateSalary(employees);
                        Console.WriteLine($"The total Salary is {salary}");
                        Console.WriteLine("______________________________________");
                        break;
                    case 5:
                        //Exit
                        choice = -1;
                        break;


                    default:
                        Console.WriteLine("Invalid choice. Please select 1 or 2.");
                        break;
                }
            }

            

        }


        // Updating Employee
        private static void UpdateEmployee()
        {
            try
            {
                
                Console.Write("Enter the Employee id :");
                bool found = false;
                int id = int.Parse(Console.ReadLine());
                Employee emp = new Employee();
                foreach (var item in employees)
                {
                    if (item.EmpId == id)
                    {
                        emp = item;
                        found = true;
                        employees = employees.Where(x => x.EmpId != id).ToList();
                        break;
                    }
                }
                if (!found)
                {
                    throw new Exception("Invalid User id Or No user with this id");
                }
                Console.WriteLine("1.Name\n2.AnnualInconme\n3.department\nWhat you whant to updat : ");
                int ch = int.Parse(Console.ReadLine());
                switch (ch)
                {
                    case 1: 
                        Console.Write("Enter new name :");
                        string newName = Console.ReadLine();
                        emp.EmpName = newName;
                        employees.Add(emp);
                        Save();
                        break;
                    case 2:
                        Console.Write("Enter new Income :");
                        double newIncome = double.Parse(Console.ReadLine());
                        emp.AnnualIncome = newIncome;
                        employees.Add(emp);
                        Save();
                        break;
                    case 3:
                        Console.Write("Enter new department :");
                        string newDept = Console.ReadLine();
                        emp.Department = newDept;
                        employees.Add(emp);
                        Save();
                        break;
                    default:
                        throw new Exception("Invalid input for Updation...");
                        break;
                }

                Console.WriteLine("Updation Success");
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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


            if (employees.Count > 0)
            {
                Console.WriteLine("Current Employees Are :");
                foreach (var item in employees)
                {
                    Console.WriteLine($"{item.EmpId} - {item.EmpName} - {item.Department} - {item.Type} - {item.AnnualIncome}");
                }
                
            }
        }


        // Adding New Employee
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

            Save();
        }

        //Function to save to Db.txt
        public static void Save()
        {
            using (StreamWriter sr = new StreamWriter("../../../db.txt"))
            {
                string data = JsonSerializer.Serialize(employees);
                sr.WriteLine(data);
            }
        }
    }
}

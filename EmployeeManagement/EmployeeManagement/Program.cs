using System.ComponentModel;
using Microsoft.Data.SqlClient;
using System.Text.Json;

namespace EmployeeManagement
{
    internal class Program
    {

        public static List<Employee> employees = new List<Employee>();
        static void Main(string[] args)
        {
            try
            {


                Console.WriteLine("Welcome To Employee Management System");
                Console.WriteLine("______________________________________");

                DisplayAll();

                Console.WriteLine("______________________________________");

                int choice = 0;
                while (choice != -1)
                {
                    Console.WriteLine("1.Add employee\n2.Update employee Details\n3.View Employees\n4.Delete employee\n5.Calculate Salary\n6.View Payroll History\n7.Report Service Menu\n8.Exit\nEnter Your Choice :"); choice = int.Parse(Console.ReadLine());
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
                            DeleteEmp();
                            Console.WriteLine("______________________________________");
                            break;
                        case 5:
                            Console.WriteLine("______________________________________");
                            SalaryProcessing.calculateSalary(employees);
                            Console.WriteLine("______________________________________");
                            break;

                        case 6:
                            Console.WriteLine("______________________________________");
                            try
                            {
                                PayrollService.display();
                            }
                            catch(Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                Console.WriteLine("______________________________________");
                            }
                            break;

                        case 7:
                            ReportServiceMenu();
                            break;

                        case 8:
                            choice = -1;
                            break;

                        default:
                            throw new Exception("Invalid Choice..");
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }


        //Delete Employee
        private static void DeleteEmp()
        {
            try
            {
                Console.Write("Enter the Employee id :");
                int id = int.Parse(Console.ReadLine());
                bool found = false;
                foreach (var item in employees)
                {
                    if (item.EmpId == id)
                    {
                        employees = employees.Where(x => x.EmpId != id).ToList();
                        found = true;
                        break;
                    }
                }
                if (found)
                {
                    Save();
                    Console.WriteLine("Deletion Successful..");
                }
                else
                {
                    throw new Exception("Employee not found");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Updation Success");


        }


        //Display All
        public static void DisplayAll()
        {
            try
            {
                String json = null;
                using (StreamReader streamReader = new StreamReader("../../../db.txt"))
                {
                    json = streamReader.ReadToEnd();
                }
                if (!string.IsNullOrWhiteSpace(json))
                {

                    List<Employee> currentdetails = new List<Employee>();
                    currentdetails = JsonSerializer.Deserialize<List<Employee>>(json);

                    employees = currentdetails;

                }
                else
                {
                    throw new Exception("Currently no employees");
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        // Adding New Employee
        public static void AddEmployee()
        {
            try
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
                    throw new Exception("Invalid input");
                }
                Console.WriteLine("Enter the employee salary :");
                double income = double.Parse(Console.ReadLine());
                SqlConnection connection = ConnectToDb();
                Console.WriteLine(connection.State);

                SqlCommand  commandsToIn =connection.CreateCommand();
                commandsToIn.CommandText = "INSERT INTO Employees (EmpId, EmpName, Income, Dept, Type) VALUES (@id, @name, @income, @dept, @type)";
                commandsToIn.Parameters.AddWithValue("@id", employees.Count + 1);
                commandsToIn.Parameters.AddWithValue("@name", name);
                commandsToIn.Parameters.AddWithValue("@income", income);
                commandsToIn.Parameters.AddWithValue("@dept", dept);
                commandsToIn.Parameters.AddWithValue("@type", type);

                int succes = commandsToIn.ExecuteNonQuery();
                connection.Close();
                if(succes > 0)
                {
                    Console.WriteLine("Added to database Also");
                }
                else
                {
                    throw new Exception("Adding to database failed..");
                }
                

                    employees.Add(new Employee(employees.Count + 1, name, income, dept, type));

                Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //Function to save to Db.txt
        public static void Save()
        {
            try
            {
                using (StreamWriter sr = new StreamWriter("../../../db.txt"))
                {
                    string data = JsonSerializer.Serialize(employees);
                    sr.WriteLine(data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static SqlConnection ConnectToDb()
        {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = "Data Source=447D79DDBACD5CB\\SQLEXPRESS;Initial Catalog=EmployeeManagementSystem;Integrated Security=True;Trust Server Certificate=True";
            sqlConnection.Open();
            return sqlConnection;
        }

        public static void ReportServiceMenu()
        {

            while (true)
            {
                Console.WriteLine("********************************");
                Console.WriteLine("1. Find Employees By Name");
                Console.WriteLine("2. Display All Employees By Department");
                Console.WriteLine("3. PaySlip");
                Console.WriteLine("4. Display All Employees By Type");
                Console.WriteLine("5. Exit");
                Console.WriteLine("Enter Choice");
                int ch = int.Parse(Console.ReadLine());
                switch (ch)
                {
                    case 1:
                        Console.WriteLine("Enter Name to search");
                        string name = Console.ReadLine();
                        ReportService.EmployeeByName(employees, name);
                        break;

                    case 2:
                        Console.WriteLine("Enter Department to search");
                        string dept = Console.ReadLine();
                        ReportService.EmployeeByDepartment(employees, dept);
                        break;

                    case 3:
                        Console.WriteLine("Enter employee ID (PaySlip)");
                        int id = int.Parse(Console.ReadLine());
                        ReportService.PaySlip(id);
                        break;

                    case 4:
                        ReportService.EmployeeByType();
                        break;

                    case 5:
                        return;

                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }
    }
}

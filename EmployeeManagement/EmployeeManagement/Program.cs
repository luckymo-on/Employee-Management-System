using System.ComponentModel;

namespace EmployeeManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {

            List<Employee> employees = new List<Employee>();

            Console.WriteLine("Welcome To Employee Management System");
            int choice = 0;
            while (choice != -1)
            {
                Console.WriteLine("1.Add employee\n2.Update employee Details\n3.View Employees\n4.Calculate Salary\n5.Exit\nEnter Your Choice :");
                choice = int.Parse(Console.ReadLine());
                if (choice == 1)
                {
                    //AddingNew Employee

                    Console.WriteLine("Enter the employee name :");
                    string name = Console.ReadLine();
                    Console.WriteLine("Enter the employee department :");
                    string dept = Console.ReadLine();
                    Console.WriteLine("Enter the employee type \na.Permanant\nb.Contract:");
                    string ch = Console.ReadLine().ToLower();
                    string type = null;
                    if (ch == "a")
                    {
                         type= "Permanent";
                    }else if (ch == "b")
                    {
                         type = "Contract";
                    }
                    else
                    {
                        Console.WriteLine("Invalid input");
                        continue;
                    }
                    Console.WriteLine("Enter the annual income :");
                    double annualIncome = double.Parse(Console.ReadLine());

                    employees.Add(new Employee(employees.Count+1,name,dept,annualIncome,type));

                }
                else if (choice == 2) 
                {
                    //Developing in process
                }
                else if (choice == 3)
                {
                    //Developing in process
                }
                else if (choice == 4)
                {
                    double salary = SalaryProcessing.calculateSalary(employees);
                    Console.WriteLine($"Your salary for this month is {salary}");

                }
            }

        }
    }
}

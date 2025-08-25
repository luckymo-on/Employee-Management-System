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
                Console.WriteLine("1.Add employee\n2.Update employee Details\n3.View Employees\n4.Exit\nEnter Your Choice :");
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
                         type= "Permenant";
                    }else if (ch == "b")
                    {
                         type = "Contract";
                    }
                    else
                    {
                        Console.WriteLine("Invalid input");
                        continue;
                    }
                    Console.WriteLine("Enter the employee salary :");
                    double salary = double.Parse(Console.ReadLine());

                    employees.Add(new Employee(employees.Count+1,name,dept,salary,type));

                }
                else
                {
                    //Developing in process
                }
            }

        }
    }
}

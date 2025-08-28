using EmployeeManagement.Models;
using EmployeeManagement.Repository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EmployeeManagement.Services
{
    public class EmployeeServices
    {

        public static List<Employee> employees = new List<Employee>();
        //Display All
        public static void DisplayAll()
        {
            try
            {

                FetchEmployees();

                if (employees.Count > 0)
                {
                    Console.WriteLine("Current Employees Are :");

                    Console.WriteLine("+--------+------------------+------------+-----------+---------------+");
                    Console.WriteLine("| EmpId  | EmpName          | Department | Type      | AnnualIncome  |");
                    Console.WriteLine("+--------+------------------+------------+-----------+---------------+");

                    // Print employee rows
                    foreach (var item in employees)
                    {
                        Console.WriteLine("| {0,-6} | {1,-16} | {2,-10} | {3,-9} | {4,13:N2} |",
                            item.EmpId, item.EmpName, item.Department, item.Type, item.AnnualIncome);
                    }

                    // Print bottom border
                    Console.WriteLine("+--------+------------------+------------+-----------+---------------+");

                }
                else
                {
                    throw new Exception("There are currently no employees");
                }
            }
            catch (Exception ex)
            {
                Ui.PrintError(ex.Message);
            }
        }



        // Adding New Employee
        public static void AddEmployee()
        {

            try
            {
                FetchEmployees();
                Console.Write("Enter the employee name (Max 16 Characters):");
                string name = Console.ReadLine();
                if (!Regex.IsMatch(name, @"^[A-Za-z\s]{1,16}$"))
                {
                    throw new Exception("Invalid Name. Name should only contain letters and spaces (max 16 characters).");
                }
                else
                {
                    name = char.ToUpper(name[0]) + name.Substring(1);
                }
                string dept = DepartmentService.PromptDepartment();
                Console.Write("Enter the employee type \np.Permanant\nc.Contract: ");
                string ch = Console.ReadLine().ToLower();
                string type = null;
                if (ch.ToLower() == "p")
                {
                    type = "Permanent";
                }
                else if (ch.ToLower() == "c")
                {
                    type = "Contract";
                }
                else
                {
                    throw new Exception("Invalid input");
                }
                if (type == "Permanent")
                {
                    Console.Write("Enter the employee annual income :");
                }
                else if (type == "Contract")
                {
                    Console.Write("Enter the Contract Amount :");
                }

                double income = double.Parse(Console.ReadLine());
                int id = 0;
                if (employees.Count > 0)
                {
                    id = employees.Max(x => x.EmpId) + 1;
                }
                else
                {
                    id++;
                }
                DatabaseRepo.SaveEmployee(id, name, income, dept, type);
                employees.Add(new Employee(id, name, income, dept, type));

                FileRepo.Save(employees);
            }
            catch (Exception ex)
            {
                Ui.PrintError(ex.Message);
            }


        }


        // Updating Employee
        public static void UpdateEmployee()
        {

            try
            {

                FetchEmployees();

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
                        break;
                    }
                }
                if (!found)
                {
                    throw new Exception("Invalid User ID or No user found with this ID");
                }else 
                if (found)
                {
                    Console.WriteLine("+--------+------------------+------------+-----------+---------------+");
                    Console.WriteLine("| EmpId  | EmpName          | Department | Type      | AnnualIncome  |");
                    Console.WriteLine("+--------+------------------+------------+-----------+---------------+");
                    Console.WriteLine("| {0,-6} | {1,-16} | {2,-10} | {3,-9} | {4,13:N2} |",
                            emp.EmpId, emp.EmpName, emp.Department, emp.Type, emp.AnnualIncome);
                    Console.WriteLine("+--------+------------------+------------+-----------+---------------+");
                }
                Console.WriteLine();
                Console.Write("[1] Name\n[2] AnnualInconme\n[3] Department\n What do you wish to update ? : ");
                int ch = int.Parse(Console.ReadLine());
                switch (ch)
                {
                    case 1:
                        Console.Write("Enter new name :");
                        string newName = Console.ReadLine();
                        if (!Regex.IsMatch(newName, @"^[A-Za-z\s]{1,16}$"))
                        {
                            throw new Exception("Invalid Name. Name should only contain letters and spaces (max 16 characters).");
                        }
                        else
                        {
                            newName = char.ToUpper(newName[0]) + newName.Substring(1);
                        }
                        employees = employees.Where(x => x.EmpId != id).ToList();
                        emp.EmpName = newName;

                        DatabaseRepo.UpdateEmployeeName(id, newName);

                        employees.Add(emp);
                        FileRepo.Save(employees);
                        Ui.PrintSuccess("Updation Success");
                        break;

                    case 2:
                        Console.Write("Enter new Income :");
                        double newIncome = double.Parse(Console.ReadLine());
                        employees = employees.Where(x => x.EmpId != id).ToList();
                        emp.AnnualIncome = newIncome;

                        DatabaseRepo.UpdateEmployeeIncome(id, newIncome);

                        employees.Add(emp);
                        FileRepo.Save(employees);
                        Console.WriteLine();
                        Ui.PrintSuccess("Updation Success");
                        break;

                    case 3:
                        Console.Write("Enter new department :");
                        string newDept = Console.ReadLine();
                        newDept = newDept.ToUpper();
                        employees = employees.Where(x => x.EmpId != id).ToList();
                        emp.Department = newDept;

                        DatabaseRepo.UpdateEmployeeDepartment(id, newDept);

                        employees.Add(emp);
                        FileRepo.Save(employees);
                        Console.WriteLine();
                        Ui.PrintSuccess("Updation Success");
                        break;

                    default:
                        throw new Exception("Invalid input for Updation...");
                }
            }
            catch (FormatException fe)
            {
                Ui.PrintError(fe.Message);
            }
            catch (Exception ex)
            {
                Ui.PrintError(ex.Message);
            }
        }

        //Delete Employee
        public static void DeleteEmp()
        {
            try
            {
                FetchEmployees();
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
                DatabaseRepo.DeleteEmployee(id);
                if (found)
                {
                    FileRepo.Save(employees);
                    Ui.PrintSuccess("Deletion Successful..");
                }
                else
                {
                    throw new Exception("Employee not found in Text File");
                }
            }
            catch (Exception ex)
            {
                Ui.PrintError(ex.Message);
            }


        }

      

        public static List<Employee> FetchEmployees()
        {
            employees = FileRepo.FetchEmployees();
            employees = employees.OrderBy(e => e.EmpId).ToList();
            if (employees == null)
            {
                employees = new List<Employee>();
            }

            foreach (var emp in employees)
            {
                DepartmentService.AddDepartment(emp.Department);
            }

            return employees;
        }

    }

    public static class DepartmentService
    {
        // This will hold all unique departments
        private static List<string> departments = new List<string>();

        // Add a department if it's new
        public static void AddDepartment(string dept)
        {
            if (!string.IsNullOrWhiteSpace(dept) &&
                !departments.Exists(d => d.Equals(dept, StringComparison.OrdinalIgnoreCase)))
            {
                departments.Add(dept.Trim());
            }
        }

        // Display current departments
        public static void DisplayDepartments()
        {
            List<Employee> temp = FileRepo.FetchEmployees();

            if (temp.Count == 0)
            {
                Console.WriteLine("No departments added yet.");
            }
            else
            {
                departments = temp.Select(x => x.Department).Distinct().ToList();
                Console.WriteLine("Current Departments : " + string.Join(", ", departments));
            }
        }

        // Prompt the user to enter a department and show existing ones
        public static string PromptDepartment()
        {
            DisplayDepartments();
            Console.Write("Enter department: ");
            string input = Console.ReadLine().Trim().ToUpper();
            AddDepartment(input);
            return input;
        }
    }
}
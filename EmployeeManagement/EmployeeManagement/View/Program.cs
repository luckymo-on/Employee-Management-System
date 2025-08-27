using EmployeeManagement.Models;
using Microsoft.Data.SqlClient;
using System.ComponentModel;
using System.Text.Json;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;
using EmployeeManagement.Services;
using EmployeeManagement.Repository;

namespace EmployeeManagement.UI
{
    public class Program
    {

        
        static void Main(string[] args)
        {
            try
            {


                Ui.PrintHeader("WELCOME TO EMPLOYEE MANAGEMENT SYSTEM");



                EmployeeServices.DisplayAll();
                Console.WriteLine();

                int choice = 0;
                while (choice != -1)
                {
                    Console.WriteLine();
                    Ui.ShowMainMenu();
                    choice = int.Parse(Console.ReadLine());

                    //Console.WriteLine("1.Add employee\n2.Update employee Details\n3.View Employees\n4.Delete employee\n5.Calculate Salary\n6.View Payroll History\n7.Report Service Menu\n8.Exit\nEnter Your Choice :"); choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            // Adding New Employee
                            
                            EmployeeServices.AddEmployee();
                            Ui.PrintSectionBorder();
                            break;

                        case 2:
                            // Updating Employee
                            
                            EmployeeServices.UpdateEmployee();
                            Ui.PrintSectionBorder();
                            break;
                        case 3:
                            
                            EmployeeServices.DisplayAll();
                            
                            break;
                        case 4:
                            
                            EmployeeServices.DeleteEmp();
                            Ui.PrintSectionBorder();
                            break;
                        case 5:
                           
                            SalaryServices.CalculateeSalaries();
                            Ui.PrintSectionBorder();
                            break;

                        case 6:
                            
                            try
                            {
                                PayRollServices.display();
                            }
                            catch(Exception ex)
                            {
                                Ui.PrintError(ex.Message);
                                Ui.PrintSectionBorder();
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

                Ui.PrintError(ex.Message);
            }
        }


        


        


        


        

        


        


        //Report Generation
        public static void ReportServiceMenu()
        {

            while (true)
            {
                Console.WriteLine("______________________________________");
                Console.WriteLine("1. Find Employees By Name");
                Console.WriteLine("2. Display All Employees By Department");
                Console.WriteLine("3. PaySlip");
                Console.WriteLine("4. Display All Employees By Type");
                Console.WriteLine("5. Exit");
                Console.Write("Enter Choice :");
                
                int ch = int.Parse(Console.ReadLine());
                switch (ch)
                {
                    case 1:
                        Console.WriteLine("______________________________________");
                        Console.Write("Enter Name to search :");
                        string name = Console.ReadLine();
                        Console.WriteLine();
                
                        ReportService.EmployeeByName( name);
                        break;

                    case 2:
                        Console.WriteLine("______________________________________");
                        Console.Write("Enter Department to search :");
                        string dept = Console.ReadLine();
                        Console.WriteLine();
                        ReportService.EmployeeByDepartment( dept);
                        break;

                    case 3:
                        Console.WriteLine("______________________________________");
                        Console.Write("Enter employee ID (PaySlip) :");
                        int id = int.Parse(Console.ReadLine());
                        Console.WriteLine();
                        ReportService.PaySlip(id);
                        break;

                    case 4:
                        Console.WriteLine("______________________________________");
                        ReportService.EmployeeByType();
                        break;

                    case 5:
                        Console.WriteLine("______________________________________");
                        return;

                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }
    }
}

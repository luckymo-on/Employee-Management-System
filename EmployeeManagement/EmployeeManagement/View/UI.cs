using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement
{
    public static class Ui
    {
        //Styling Error message
        public static void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;   
            Console.WriteLine("ERROR: " + message);
            Console.ResetColor();
        }


        //Styling Success message
        public static void PrintSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        // Method to print a header with borders
        static int width = Console.WindowWidth;
        public static void PrintHeader(string title)
        {
            
            Console.Clear();
            Console.WriteLine(new string('-', width));
            Console.WriteLine(String.Format("{0," + ((width / 2) + (title.Length / 2)) + "}", title));
            Console.WriteLine(new string('-', width));
            Console.WriteLine();
        }

        // Method to print the main menu
        public static void ShowMainMenu()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("==== MAIN MENU ====");
            Console.ResetColor();

            Console.WriteLine("[1] Add Employee");
            Console.WriteLine("[2] Update Employee");
            Console.WriteLine("[3] View Employees");
            Console.WriteLine("[4] Delete Employee");
            Console.WriteLine("[5] Calculate Salary");
            Console.WriteLine("[6] View Payroll History");
            Console.WriteLine("[7] Generate Report");
            Console.WriteLine("[8] Exit");

            Console.WriteLine();
            Console.Write("Enter Your Choice: ");
        }

        public static void PrintSectionBorder()
        {
            Console.WriteLine(new string('-',width));
        }
    }
}

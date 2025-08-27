using EmployeeManagement;
using EmployeeManagement.Models;
using EmployeeManagement.Services;

namespace EMS_Test
{
    public class UnitTest1
    {

       //SalaryProcessing.CalculateSalary() test
       //Testing if employee exists to calculate salary
        [Fact]
        public void CalculateSalary_WhenEmployeeNotFound_ReturnsZero()
        {
            
            var employees = new List<Employee>();

            using var input = new StringReader("99\n"); // ID not in list
            Console.SetIn(input);

            using var output = new StringWriter();
            Console.SetOut(output);

            
            double salary = SalaryServices.calculateSalary(employees);

            
            Assert.Equal(0, salary);
            Assert.Contains("Employee not found", output.ToString());
        }

        //ReportService.EmployeeByName() test
        //testing if filtering by name works

        //if employee found
        [Fact]
        public void EmployeeByName_ShouldPrintEmployee_WhenNameMatches()
        {
            
            var employees = new List<Employee>
            {
                new Employee { EmpId = 1, EmpName = "Alice", Department = "IT", Type = "Permanent", AnnualIncome = 60000 }
            };

            using var sw = new StringWriter();
            Console.SetOut(sw);

            
            ReportService.EmployeeByName(employees, "Ali");

            
            var output = sw.ToString();
            Assert.Contains("Alice", output);
            Assert.Contains("Employees with name containing", output);
        }

        // if there is no employee after filtering
        [Fact]
        public void EmployeeByDepartment_ShouldPrintMessage_WhenNoEmployees()
        {
            var employees = new List<Employee>();

            using var sw = new StringWriter();
            Console.SetOut(sw);

            ReportService.EmployeeByDepartment(employees, "IT");

            var output = sw.ToString();
            Assert.Contains("No employees found in department 'IT'", output);
        }

        ///ReportService.EmployeeByDept() test
        //testing if filtering by dept works

        [Fact]
        public void EmployeeByDepartment_ShouldPrintEmployee_WhenDepartmentMatches()
        {
            var employees = new List<Employee>
            {
                new Employee { EmpId = 2, EmpName = "B", Department = "HR", Type = "Contract", AnnualIncome = 0 }
            };

            using var sw = new StringWriter();
            Console.SetOut(sw);

            ReportService.EmployeeByDepartment(employees, "HR");

            var output = sw.ToString();
            Assert.Contains("B", output);
            Assert.Contains("Employees in department 'HR'", output);
        }
    }
}




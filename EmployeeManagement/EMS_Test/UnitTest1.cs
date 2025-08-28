using EmployeeManagement;
using EmployeeManagement.Models;
using EmployeeManagement.Services;


namespace EMS_Test
{
    public class UnitTest1
    {

        //SalaryProcessing.CalculateSalary() test

        //Testing if employee exists to calculate salary

        //no employee exists

        [Fact]
        public void CalculateSalary_ShouldPrintError_IfNoEmployee()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            SalaryServices.CalculateeSalaries();

            Assert.Contains("Error while generating salaries", output.ToString());

        }

        //Employee test

        //Checking if employee stores values correctly
        [Fact]
        public void Employee_ShouldStoreValuesCorrectly()
        {
            var emp = new Employee(101, "Alice", 60000, "HR", "Permanent");

            Assert.Equal(101, emp.EmpId);
            Assert.Equal("Alice", emp.EmpName);
            Assert.Equal("HR", emp.Department);
            Assert.Equal("Permanent", emp.Type);
            Assert.Equal(60000, emp.AnnualIncome);
        }

        //test to check if default constructor gives default values
        [Fact]
        public void Employee_DefaultConstructor_ShouldHaveDefaultValues()
        {
            var emp = new Employee();

            Assert.Equal(0, emp.EmpId);
            Assert.Null(emp.EmpName);
            Assert.Null(emp.Department);
            Assert.Null(emp.Type);
            Assert.Equal(0, emp.AnnualIncome);
        }

        //tests to see if employee values can be updated
        [Fact]
        public void Employee_CanChangeProperties()
        {

            var emp = new Employee();

            emp.EmpId = 202;
            emp.EmpName = "Bob";
            emp.Department = "IT";
            emp.Type = "Contract";
            emp.AnnualIncome = 50000;

            Assert.Equal(202, emp.EmpId);
            Assert.Equal("Bob", emp.EmpName);
            Assert.Equal("IT", emp.Department);
            Assert.Equal("Contract", emp.Type);
            Assert.Equal(50000, emp.AnnualIncome);
        }

        //ReportServices test

        //test to see if filtering by employee name works

        [Fact]
        public void FilterByName_ShouldReturnEmployees()
        {
            var employees = new List<Employee>
        {
            new Employee(1, "Alice", 60000, "HR", "Permanent"),
            new Employee(2, "Sam", 50000, "IT", "Contract"),
            new Employee(3, "Alicia", 45000, "IT", "Permanent")
        };

            var results = employees
                .Where(e => e.EmpName.Contains("Ali", System.StringComparison.OrdinalIgnoreCase))
                .ToList();

            Assert.Equal(2, results.Count);
            Assert.Contains(results, e => e.EmpName == "Alice");
            Assert.Contains(results, e => e.EmpName == "Alicia");
        }

        //test to see if filtering by department works
        [Fact]
        public void FilterByDepartment_ShouldReturnEmployees()
        {
            // Arrange
            var employees = new List<Employee>
        {
            new Employee(1, "Alice", 60000, "HR", "Permanent"),
            new Employee(2, "Sam", 50000, "IT", "Contract"),
            new Employee(3, "Anu", 45000, "HR", "Permanent")
        };

            var results = employees
                .Where(e => e.Department.Equals("HR", System.StringComparison.OrdinalIgnoreCase))
                .ToList();

            Assert.Equal(2, results.Count);
            Assert.Contains(results, e => e.EmpName == "Alice");
            Assert.Contains(results, e => e.EmpName == "Anu");
        }
        //Payroll tests

        //checking if payroll gets added

        [Fact]
        public void ExistsReturnTrue_WhenPayrollExists()
        {
            var backup = new List<Payroll>(PayRollServices.payroll);
            try
            {
                PayRollServices.payroll.Clear();
                PayRollServices.payroll.Add(new Payroll(1, 107, "Alice", "HR", "Permanent", 5000, 1000, 500, null, null,5500, DateOnly.FromDateTime(DateTime.Now)));

                bool result = PayRollServices.Exists(107);

                Assert.True(result);

            }
            finally
            {
                PayRollServices.payroll = backup;
            }
        }
        [Fact]
        public void ExistsReturnFalse_WhenPayrollExists()
        {
            var backup = new List<Payroll>(PayRollServices.payroll);
            try
            {
                PayRollServices.payroll.Clear();

                bool result = PayRollServices.Exists(995);

                Assert.False(result);

            }
            finally
            {
                PayRollServices.payroll = backup;
            }
        }
    }

}



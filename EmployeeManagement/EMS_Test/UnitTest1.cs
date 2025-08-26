using EmployeeManagement;

namespace EMS_Test
{
    public class UnitTest1
    {

       
        [Fact]
        public void CalculateSalary_WhenEmployeeNotFound_ReturnsZero()
        {
            
            var employees = new List<Employee>();

            using var input = new StringReader("99\n"); // ID not in list
            Console.SetIn(input);

            using var output = new StringWriter();
            Console.SetOut(output);

            
            double salary = SalaryProcessing.calculateSalary(employees);

            
            Assert.Equal(0, salary);
            Assert.Contains("Employee not found", output.ToString());
        }
    }
}



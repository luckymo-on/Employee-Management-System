using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmployeeManagement.Repository
{
    public class FileRepo
    {

        //Function to save to Db.txt
        public static void Save(List<Employee> employees)
        {
            try
            {
                using (StreamWriter sr = new StreamWriter("../../../Employees.txt"))
                {
                    if (employees.Count > 0)
                    {
                        string data = JsonSerializer.Serialize(employees, new JsonSerializerOptions { WriteIndented=false});
                        sr.WriteLine(data);
                    }
                    else
                    {
                        sr.WriteLine("");
                    }


                }
            }
            catch (Exception ex)
            {
                Ui.PrintError(ex.Message);
            }
        }

        //Fetching Payrolles
        public static List<Payroll> Fetch()
        {
            List<Payroll> temp = new List<Payroll>();
            using (StreamReader sr = new StreamReader("../../../PayrollHistory.txt"))
            {
                string data;
                while ((data = sr.ReadLine()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(data))
                    {
                        Payroll record = JsonSerializer.Deserialize<Payroll>(data);
                        temp.Add(record);
                    }
                }
            }
            return temp;
        }

        //Saving PayRoll to File
        public static void SaveTOFile(Payroll newPayroll,List<Payroll> payroll)
        {

            
            using (StreamWriter sw = new StreamWriter("../../../PayrollHistory.txt"))
            {
                foreach (var record in payroll)
                {
                    string data = JsonSerializer.Serialize(record, new JsonSerializerOptions { WriteIndented = false });
                    sw.WriteLine(data);
                }

            }
        }

        public static List<Employee> FetchEmployees()
        {
            string json = null;
            using (StreamReader streamReader = new StreamReader("../../../Employees.txt"))
            {
                json = streamReader.ReadToEnd();
            }
            if (!string.IsNullOrWhiteSpace(json))
            {

                List<Employee> currentdetails = new List<Employee>();
                currentdetails = JsonSerializer.Deserialize<List<Employee>>(json);

                
                return currentdetails;

            }
            return null;
        }
    }
}

using EmployeeManagement.Models;
using EmployeeManagement.UI;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Repository
{
    public class DatabaseRepo
    {

        //Extablising Connection
        public static SqlConnection ConnectToDb()
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection();
                sqlConnection.ConnectionString = "Data Source=03A0EFA6CC12509\\SQLEXPRESS;Initial Catalog=EMS_DB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
                sqlConnection.Open();
                return sqlConnection;
            }
            catch (Exception ex)
            {
                Ui.PrintError(ex.Message);

                return null;
            }

        }


        //Saving PayRoll to DB
        public static void SaveToDb(Payroll newPayroll)
        {
            

            using (SqlConnection connection = ConnectToDb())//Calling the function from program class
            { 
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "INSERT INTO Payroll (PayrollId, EmployeeId, EmpName, Department, Type, BasicPay, Allowance, Deductions, Hours, HourlyRate, Salary, PaymentDate) VALUES (@PayrollId, @EmployeeId, @EmpName, @Department, @Type, @BasicPay, @Allowance, @Deductions, @Hours, @HourlyRate, @Salary, @PaymentDate)";
                cmd.Parameters.AddWithValue("@PayrollId", newPayroll.PayrollId);
                cmd.Parameters.AddWithValue("@EmployeeId", newPayroll.EmployeeId);
                cmd.Parameters.AddWithValue("@EmpName", newPayroll.EmpName);
                cmd.Parameters.AddWithValue("@Department", newPayroll.Department);
                cmd.Parameters.AddWithValue("@Type", newPayroll.Type);
                cmd.Parameters.AddWithValue("@BasicPay", newPayroll.BasicPay ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Allowance", newPayroll.Allowance ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Deductions", newPayroll.Deductions ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Hours", newPayroll.Hours ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@HourlyRate", newPayroll.HourlyRate ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Salary", newPayroll.Salary);
                cmd.Parameters.AddWithValue("@PaymentDate", newPayroll.PaymentDate.ToDateTime(TimeOnly.MinValue));

                cmd.ExecuteNonQuery();
            }


        }


        //Add New Employee to DataBase
        public static void SaveEmployee(int id, string name, double income, string dept, string type)
        {
            SqlConnection connection = ConnectToDb();
            SqlCommand commandsToIn = connection.CreateCommand();
            commandsToIn.CommandText = "INSERT INTO Employees (EmpId, EmpName, Income, Dept, Type) VALUES (@id, @name, @income, @dept, @type)";
            commandsToIn.Parameters.AddWithValue("@id", id);
            commandsToIn.Parameters.AddWithValue("@name", name);
            commandsToIn.Parameters.AddWithValue("@income", income);
            commandsToIn.Parameters.AddWithValue("@dept", dept);
            commandsToIn.Parameters.AddWithValue("@type", type);

            int succes = commandsToIn.ExecuteNonQuery();
            connection.Close();
            if (succes > 0)
            {
                Ui.PrintSuccess("Employee Added Succesfully");
            }
            else
            {
                throw new Exception("Adding to database failed..");
            }
        }



        //Updating Name
        public static void UpdateEmployeeName(int id,string newName)
        {
            using (SqlConnection connection = ConnectToDb()) 
            {

                SqlCommand commandToGetEmpById = connection.CreateCommand();
                commandToGetEmpById.CommandText = "select * from Employees where EmpId = @id";
                commandToGetEmpById.Parameters.AddWithValue("@id", id);
                using (SqlDataReader reader = commandToGetEmpById.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new Exception("Employee not found in DataBase");
                    }
                }

                SqlCommand commandToUpdateName = connection.CreateCommand();
                commandToUpdateName.CommandText = "update Employees set EmpName = @name where EmpId = @id";
                commandToUpdateName.Parameters.AddWithValue("@name", newName);
                commandToUpdateName.Parameters.AddWithValue("@id", id);
                if (commandToUpdateName.ExecuteNonQuery() != 1)
                {
                    throw new Exception("Updation in Db Failed");
                }

            }
                
        }


        //Updating Income
        public static void UpdateEmployeeIncome(int id, double newIncome)
        {
            using (SqlConnection connection = ConnectToDb())
            {

                SqlCommand commandToGetEmpById = connection.CreateCommand();
                commandToGetEmpById.CommandText = "select * from Employees where EmpId = @id";
                commandToGetEmpById.Parameters.AddWithValue("@id", id);
                using (SqlDataReader reader = commandToGetEmpById.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new Exception("Employee not found in Database");
                    }
                }

                SqlCommand commandToUpdateIncome = connection.CreateCommand();
                commandToUpdateIncome.CommandText = "update Employees set Income = @income where EmpId = @id";
                commandToUpdateIncome.Parameters.AddWithValue("@income", newIncome);
                commandToUpdateIncome.Parameters.AddWithValue("@id", id);
                if (commandToUpdateIncome.ExecuteNonQuery() != 1)
                {
                    throw new Exception("Updation in Database Failed");
                }

            }

        }


        //Update Department
        public static void UpdateEmployeeDepartment(int id, string newDept)
        {
            using (SqlConnection connection = ConnectToDb())
            {

                SqlCommand commandToGetEmpById = connection.CreateCommand();
                commandToGetEmpById.CommandText = "select * from Employees where EmpId = @id";
                commandToGetEmpById.Parameters.AddWithValue("@id", id);
                using (SqlDataReader reader = commandToGetEmpById.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new Exception("Employee not found in DataBase");
                    }
                }

                SqlCommand commandToUpdateIncome = connection.CreateCommand();
                commandToUpdateIncome.CommandText = "update Employees set Dept = @dept where EmpId = @id";
                commandToUpdateIncome.Parameters.AddWithValue("@dept", newDept);
                commandToUpdateIncome.Parameters.AddWithValue("@id", id);
                if (commandToUpdateIncome.ExecuteNonQuery() != 1)
                {
                    throw new Exception("Updation in Db Failed");
                }

            }

        }


        //Delete Employee
        public static void DeleteEmployee(int id)
        {
            using (SqlConnection connection = ConnectToDb())
            {

                SqlCommand commandToGetEmpById = connection.CreateCommand();
                commandToGetEmpById.CommandText = "select * from Employees where EmpId = @id";
                commandToGetEmpById.Parameters.AddWithValue("@id", id);
                using (SqlDataReader reader = commandToGetEmpById.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new Exception("Employee not found in DataBase");
                    }
                }

                SqlCommand commandToDelete = connection.CreateCommand();
                commandToDelete.CommandText = "delete from Employees where EmpId = @id";
                commandToDelete.Parameters.AddWithValue("@id", id);
                if (commandToDelete.ExecuteNonQuery() != 1)
                {
                    throw new Exception("Deletion from DataBase failed");
                }

            }

        }


    }
}

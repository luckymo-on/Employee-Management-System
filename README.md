# Employee Management System

A simple C# console-based application for managing employees with SQL Server database integration.
This project demonstrates CRUD operations (Create, Read, Update, Delete) using ADO.NET.

#Features

  Add Employee – Supports both Permanent and Contract employees

  View Employees – Displays employee records in a well-formatted table for better readability

  Update Employee – Modify existing employee details

  Delete Employee – Remove employee records safely

  Salary Calculation

   -> Permanent employees → Base Salary + Allowances – Deductions

   -> Contract employees → Hourly Rate × Hours Worked

   -> Validations

   -> Prevents duplicate Employee IDs

   -> Rejects invalid/negative values

  Unit Tests

   -> Covers salary calculation logic

   -> Verifies data validation (e.g., no negative hours, no duplicate IDs)

   -> Ensures stable CRUD operations

#Tech Stack

-> Language: C# (.NET 6 / .NET Framework supported)

-> Database: SQL Server

-> Database Access: ADO.NET (SqlConnection, SqlCommand, etc.)

-> IDE: Visual Studio 

-> Testing: xUnit 

#How to Run

-> Clone this repository

    git clone https://github.com/your-username/EmployeeManagementSystem.git
    cd EmployeeManagementSystem


-> Open the project in Visual Studio.

-> Update the connection string in DbConnection.cs if needed:

    "Data Source=YOUR_SERVER\\SQLEXPRESS;Initial Catalog=EMS_DB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"


-> Run the program 

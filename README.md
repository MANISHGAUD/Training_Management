# Training Management System (.NET Core MVC)

This project is a Training Management System using ASP.NET Core MVC and Entity Framework (Database First).

## Features
- Add/Edit/Delete Training sessions
- Assign multiple employees from selected organizations
- Prevent duplicate trainings on same date for same employee
- Search and sort trainings using DataTables
- View training details and assigned employees

## Technologies
- ASP.NET Core MVC
- Entity Framework Core (Database First)
- SQL Server
- Bootstrap, jQuery, DataTables

## Getting Started
1. Clone the repo
2. Update the `appsettings.json` connection string
3. Run the app in Visual Studio

## Database
Use the SQL schema from the `/Docs` folder or run the script in `Program.cs` to generate the database.

CREATE TABLE Organization (
    OrganizationId INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100) NOT NULL
);

CREATE TABLE Employee (
    EmployeeId INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100) NOT NULL,
    OrganizationId INT FOREIGN KEY REFERENCES Organization(OrganizationId)
);

CREATE TABLE Training (
    TrainingId INT PRIMARY KEY IDENTITY,
    OrganizationId INT NOT NULL,
    TrainingDate DATE NOT NULL,
    Place NVARCHAR(100),
    Purpose NVARCHAR(200)
);

CREATE TABLE TrainingEmployee (
    TrainingEmployeeId INT PRIMARY KEY IDENTITY,
    TrainingId INT FOREIGN KEY REFERENCES Training(TrainingId),
    EmployeeId INT FOREIGN KEY REFERENCES Employee(EmployeeId)
);

CREATE UNIQUE INDEX UX_Employee_TrainingDate 
ON TrainingEmployee(EmployeeId, TrainingId);


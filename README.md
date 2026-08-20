# IT_ELECTIVE_PREFINALS_PROJECT

## Project Name
IT_ELECTIVE_PREFINALS_PROJECT

## Technology
- ASP.NET Core MVC
- .NET 8
- Entity Framework Core 8
- SQLite
- Razor Views
- LINQ
- Built-in Dependency Injection

## NuGet Packages
- Microsoft.EntityFrameworkCore.Sqlite 8.0.0

## Database Location
The supplied database is located at the project root:

`lycevm.db`

The application connects using `Data Source=lycevm.db`.

## Important Assignment Rules
- The supplied SQLite database is the source of truth.
- Entity classes were manually modeled from the database schema.
- EF Core database scaffolding is not used.
- Migrations are not used to create or modify the database.
- No seed data is added.

## How to Run
1. Install the .NET 8 SDK.
2. Clone this repository.
3. Open a terminal in the project directory.
4. Restore packages:

   `dotnet restore`

5. Run:

   `dotnet run`

6. Open the HTTPS/HTTP URL printed by ASP.NET Core.

## Main Pages
- Departments
- Employees
- Teams
- Customers
- Tickets
- Ticket Details
- Employee Workload
- Department Workload
- Unassigned Tickets
- Multiple-Assignee Tickets
- Primary Assignee
- Category Hierarchy

## Git Workflow
The permanent `main` branch should receive changes only through reviewed Pull Requests. Feature work should be performed on branches and submitted as Pull Requests for partner review before merging.

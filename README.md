# TaskTracker API

TaskTracker is a simple ASP.NET Core 8 Web API for managing users, tasks, and comments. It uses SQLite for persistence, AutoMapper for DTO mapping, FluentValidation for input validation, NLog for logging, and includes unit tests with xUnit and Moq.

**Build & Run Instructions:** Clone the repository `git clone https://github.com/Goga1107/TaskTracker.git` and navigate into it. Open `TaskTracker.sln` in Visual Studio 2022 or newer. Restore dependencies with `dotnet restore`, build the project with `dotnet build`, and run the API using `dotnet run --project TaskTracker.Api` or by pressing F5 in Visual Studio. Once running, access Swagger UI at `https://localhost:5001/swagger` or `http://localhost:5000/swagger` or `http://localhost:7021/swagger`.

**Features:** CRUD endpoints for Users, Tasks, and Comments; SQLite database integration; AutoMapper and FluentValidation; global exception handling middleware; environment-based configuration; Swagger/OpenAPI documentation; unit tests using xUnit and Moq; cascade delete for tasks and comments; logging via NLog.

**Developer Notes:** The database file `tasks.db` is auto-created in the project root. Logging levels are configured per environment. FluentValidation ensures that task titles are required, status values are limited to New, InProgress, Completed, and UserId must be positive.

**Technologies Used:** ASP.NET Core 8, SQLite (via Dapper), AutoMapper, FluentValidation, NLog, xUnit, Moq, Swagger/OpenAPI.

**Author:** Goga Pavliashvili  
**GitHub Repository:** [https://github.com/Goga1107/TaskTracker](https://github.com/Goga1107/TaskTracker)
Note: SQLite does not support stored procedures and has limited transaction management compared to SQL Server. Therefore, features such as “Get all tasks for a user”, “Count tasks per status”, “Get recent comments (7 days)”, and transaction-based creation of tasks with comments were implemented using standard SQL queries and manual transaction handling where possible.

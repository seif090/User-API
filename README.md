📘 User-API

A RESTful API built with ASP.NET Core that manages Users, Roles, and Authentication.
This project provides endpoints for user registration, login, role management, and user CRUD operations with role-based access.

🚀 Features

Authentication & Authorization

Register new users

User login with JWT authentication

Role-based access control

Role Management

Create, update, delete, and fetch roles

User Management

Full CRUD operations on users

Admin-only protected routes

API Documentation

Integrated Swagger UI for testing endpoints

🛠 Tech Stack

.NET 8 / ASP.NET Core Web API

Entity Framework Core / ADO.NET (depending on implementation)

SQL Server (as the database)

JWT (JSON Web Token) for authentication

Swagger / Swashbuckle for API documentation

📂 Project Structure
User-API/
│── Controllers/      # API Controllers (Auth, Role, User)
│── DTOs/             # Data Transfer Objects
│── Models/           # Database Models
│── Repositories/     # Data Access Layer
│── Services/         # Business Logic Layer
│── Program.cs        # Entry point
│── appsettings.json  # Configuration file

⚡ Getting Started
Prerequisites

.NET 8 SDK

SQL Server

Visual Studio 2022
 or VS Code

Installation & Run

Clone the repository

git clone https://github.com/seif090/User-API.git
cd User-API


Configure Database Connection
Update your appsettings.json with the correct SQL Server connection string:

"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=UserDb;Trusted_Connection=True;TrustServerCertificate=True;"
}


Apply Migrations (if using EF Core)

dotnet ef database update


Run the project

dotnet run


Access Swagger

Navigate to: https://localhost:44314/swagger/index.html

📖 API Endpoints
🔑 Auth

POST /api/Auth/register → Register a new user

POST /api/Auth/login → Login and get JWT token

👥 User

GET /api/User → Get all users

GET /api/User/{id} → Get user by ID

POST /api/User → Create new user

PUT /api/User → Update user

DELETE /api/User/{id} → Delete user

GET /api/User/admin-only → Protected endpoint (Admin only)

🔒 Role

GET /api/Role → Get all roles

GET /api/Role/{id} → Get role by ID

POST /api/Role → Create role

PUT /api/Role → Update role

DELETE /api/Role/{id} → Delete role

🧪 Example Requests

Login Request

POST /api/Auth/login
{
  "username": "admin",
  "password": "Admin@123"
}


Register Request

POST /api/Auth/register
{
  "username": "john_doe",
  "password": "Password@123",
  "role": "User"
}


Use the returned JWT token in the Authorization header:

Authorization: Bearer <your-token>

📜 License

This project is licensed under the MIT License.

👤 Author

Saif Eldin Tarek
Full-Stack .NET Developer

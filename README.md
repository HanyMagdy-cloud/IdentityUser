# 🛡️ UserIdentity.EF - ASP.NET Core JWT Authentication with Roles and Blogs

This project is a simple ASP.NET Core Web API that demonstrates:

- JWT-based authentication
- Role-based authorization (`Admin` & `Customer`)
- User registration and login
- Creating and managing blog posts
- Protected endpoints accessible only by Admins

---

## 📁 Project Structure

```bash
UserIdentity.EF/
│
├── Controllers/              # API controllers (Auth and Blogs)
├── Dtos/                     # Data Transfer Objects
├── Auth/                     # JWT generation logic
├── Interfaces/               # Blog interface (IBlogs)
├── Repos/                    # Blog repository (BlogsRepos)
├── Data/                     # Entity Framework DbContext
├── Program.cs                # Application entry point and DI setup
└── appsettings.json          # Configuration (JWT, DB, etc.)

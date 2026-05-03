# 🛒 Shop - ASP.NET Core Clean Architecture E-Commerce

A modular e-commerce web application built with ASP.NET Core (.NET 10), Clean Architecture, and PostgreSQL.

It includes an admin panel, product management, shopping cart, orders, payments, blog system, and background jobs.

---

## 🚀 Tech Stack

* ASP.NET Core (.NET 10)
* Entity Framework Core 10
* PostgreSQL (Npgsql)
* Clean Architecture
* Cookie Authentication (Role-based Authorization)
* Hangfire (Background Jobs)
* Swagger (API Documentation)

---

## ✨ Features

* Clean Architecture (Domain / Application / Infrastructure / Presentation)
* Admin panel (Products, Categories, Blog, Users)
* Role-based authentication & authorization
* Shopping cart system
* Order & payment system (ZarinPal / IDPay integration)
* Background jobs with Hangfire
* Blog system with SEO-friendly routing
* Pagination, search, and filtering

---

## 📁 Project Structure

```
Shop/
├── EndPoint.Site (Presentation Layer)
│   ├── Controllers
│   ├── Areas (Admin)
│   ├── Views
│   ├── wwwroot
│   ├── ViewComponents
│   ├── Models
│   └── RestApis
│
├── Shop.Application
│   ├── Dtos
│   ├── Interfaces
│   └── Services
│
├── Shop.Domain
│   └── Entities
│
├── Shop.Infrastructure
│   ├── Contexts
│   ├── Migrations
│   └── Persistence
│
├── Shop.Common
│   ├── Helpers
│   ├── Roles
│   └── Utilities
```

> Infrastructure layer contains implementations for external dependencies such as database access (EF Core) and third-party services.

---

## ⚙️ How to Run

### 1. Clone repository

```bash
git clone <repo-url>
cd Shop
```

---

### 2. Configure database
### 2. Configure database

Update connection string in `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "YOUR_CONNECTION_STRING"
}
```

Update connection string in `EndPoint.Site/Program.cs`:

```csharp
Host=127.0.0.1;Port=5432;Database=ShopDB;Username=postgres;Password=123456
```

---

### 3. Install EF tools (if needed)

```bash
dotnet tool install --global dotnet-ef
```

---

### 4. Run migrations

```bash
dotnet ef database update \
--project Shop.Infrastructure \
--startup-project EndPoint.Site \
--context DataBaseContext
```

---

### 5. Run project

```bash
dotnet run --project EndPoint.Site
```

---

## 🔐 Authentication

* Cookie-based authentication
* Role-based policies:

  * Admin
  * Operator
  * Customer
  * Author

Configured in `Program.cs`

---

## 🧠 Architecture Notes

This project follows Clean Architecture:

### Domain

* Core entities and business rules

### Application

* DTOs, interfaces, services, business logic

### Infrastructure

* EF Core DbContext
* Database access
* Migrations

### EndPoint.Site

* ASP.NET Core MVC UI
* Controllers / Views / REST APIs
* Dependency Injection setup

### Common

* Shared helpers, roles, DTO utilities

---

## 🎯 Purpose

This project demonstrates:
- Clean Architecture in ASP.NET Core
- Scalable e-commerce backend design
- Integration with payment gateways
- Background job processing using Hangfire

---

## 🧪 Future Improvements

- Docker support
- Unit & Integration tests
- CI/CD pipeline (GitHub Actions)
- Caching (Redis)

---

## 🔧 Important Notes

* Migrations must be executed from Infrastructure project
* Startup project is EndPoint.Site
* DbContext is located in Shop.Infrastructure
* Use EF CLI instead of Add-Migration in PowerShell

---

## 🚀 Getting Started (Quick Run)

```bash
git clone <repo-url>
cd Shop
dotnet ef database update --project Shop.Infrastructure --startup-project EndPoint.Site
dotnet run --project EndPoint.Site

---

## 📌 License

MIT License

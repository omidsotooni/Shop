# 🛒 Shop - ASP.NET Core Clean Architecture E-Commerce

A modular e-commerce web application built with ASP.NET Core 6, Clean
Architecture, and PostgreSQL.

It includes an admin panel, product management, shopping cart, orders,
payments, blog system, and background jobs.

------------------------------------------------------------------------

## 🚀 Tech Stack

-   ASP.NET Core 6
-   Entity Framework Core
-   PostgreSQL
-   Clean Architecture
-   Identity (Authentication & Authorization)
-   Hangfire (Background Jobs)
-   Swagger (API Documentation)

------------------------------------------------------------------------

## ✨ Features

-   Clean Architecture (Domain / Application / Infrastructure /
    Presentation)
-   Admin panel (Products, Categories, Blog, Users)
-   User authentication & role management
-   Shopping cart system
-   Order & payment system (ZarinPal / IDPay integration)
-   Background jobs with Hangfire
-   Blog system with SEO optimization
-   Pagination, search, and filtering

------------------------------------------------------------------------

## 📁 Project Structure

-   Shop.Domain
-   Shop.Application
-   Shop.Infrastructure
-   Shop.Persistence
-   Shop.Presentation (Web UI)

------------------------------------------------------------------------

## ⚙️ How to Run

1.  Clone the repository

2.  Install PostgreSQL

3.  Configure connection string in `appsettings.json` or use environment
    variables

4. Installing Requirement in all layers
```
    Common:
        PackageReference:
            Install-Package Microsoft.AspNetCore.Cryptography.KeyDerivation -Version 6.0.4
            
    Application
      PackageReference:
            Install-Package Microsoft.EntityFrameworkCore -Version 6.0.4
      ProjectReference:
            Shop.Common\Shop.Common.csproj
            Shop.Domain\Shop.Domain.csproj

    Domain
      PackageReference:

      ProjectReference:

    Infrastructure
      PackageReference:

      ProjectReference:

    Persistence
      PackageReference:
            Install-Package Microsoft.EntityFrameworkCore -Version 6.0.4
            Install-Package Microsoft.EntityFrameworkCore.Relational -Version 6.0.4
            Install-Package Microsoft.EntityFrameworkCore.Tools -Version 6.0.4
            Install-Package Npgsql.EntityFrameworkCore.PostgreSQL -Version 6.0.4
      ProjectReference:
            Shop.Application\Shop.Application.csproj
            
    EndPoint.Site:
       PackageReference:
            Install-Package Microsoft.EntityFrameworkCore -Version 6.0.4
            Install-Package Microsoft.EntityFrameworkCore.Design -Version 6.0.4
            Install-Package Microsoft.VisualStudio.Web.CodeGeneration.Design -Version 6.0.4
            Install-Package Npgsql.EntityFrameworkCore.PostgreSQL -Version 6.0.4
            Install-Package Swashbuckle.AspNetCore -Version 6.0.4            
       ProjectReference:
            Shop.Presentation\Shop.Persistence.csproj
```

5.  Run database migrations:

        dotnet ef database update

6.  Run the project:

        dotnet run

------------------------------------------------------------------------

## 🔐 Configuration

Use: - Environment Variables

ConnectionString : ``` @"Host=127.0.0.1;Port=5432;Database=ShopDB;Username=postgres;Password=123456" ```

------------------------------------------------------------------------

## 📌 Notes

This project is built for learning and portfolio purposes using Clean
Architecture principles in ASP.NET Core.

------------------------------------------------------------------------

## 📄 License

MIT License

# Shop With ASP.NET Core by PostgreSQL and EF Core | Clean Architecture

DataBase is PostgreSQL and DotNetCore is .Net6.0

Installing Requirement in all layers

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

ConnectionString : ``` @"Host=127.0.0.1;Port=5432;Database=ShopDB;Username=postgres;Password=123456" ```


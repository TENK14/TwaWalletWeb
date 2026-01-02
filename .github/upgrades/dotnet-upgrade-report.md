# .NET 10 Upgrade Report

## Project target framework modifications

| Project name              | Old Target Framework            | New Target Framework | Commits  |
|:--------------------------|:-------------------------------:|:--------------------:|----------|
| Model\\Model.csproj       | .NETFramework,Version=v4.8      | net10.0              | 10ef44a7 |
| Entity\\Entity.csproj     | .NETFramework,Version=v4.8      | net10.0              | 10ef44a7 |
| Web\\Web.csproj           | net48                           | net10.0              | 10ef44a7 |

## NuGet Packages

Následující NuGet balíčky byly aktualizovány nebo odstraněny během upgradu:

### Aktualizované balíčky

| Package Name                                          | Old Version    | New Version | Commit Id |
|:------------------------------------------------------|:--------------:|:-----------:|-----------|
| Microsoft.AspNetCore.Cryptography.Internal            | 2.0.1; 2.1.0   | 10.0.1      | 10ef44a7  |
| Microsoft.AspNetCore.Cryptography.KeyDerivation       | 2.0.1; 2.1.0   | 10.0.1      | 10ef44a7  |
| Microsoft.AspNetCore.DataProtection                   | 2.0.1          | 10.0.1      | 10ef44a7  |
| Microsoft.AspNetCore.DataProtection.Abstractions      | 2.0.1          | 10.0.1      | 10ef44a7  |
| Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore  | 2.0.1          | 10.0.1      | 10ef44a7  |
| Microsoft.AspNetCore.Http                             | 2.0.1          | 2.3.0       | 10ef44a7  |
| Microsoft.AspNetCore.Http.Features                    | 2.0.1; 2.0.2   | 5.0.17      | 10ef44a7  |
| Microsoft.AspNetCore.Identity                         | 2.0.1          | 2.3.1       | 10ef44a7  |
| Microsoft.AspNetCore.Identity.EntityFrameworkCore     | 2.0.1          | 10.0.1      | 10ef44a7  |
| Microsoft.AspNetCore.WebUtilities                     | 2.0.1          | 10.0.1      | 10ef44a7  |
| Microsoft.CSharp                                      | 4.4.0          | 4.7.0       | 10ef44a7  |
| Microsoft.EntityFrameworkCore                         | 2.1.0          | 10.0.1      | 10ef44a7  |
| Microsoft.EntityFrameworkCore.Abstractions            | 2.1.0          | 10.0.1      | 10ef44a7  |
| Microsoft.EntityFrameworkCore.Analyzers               | 2.1.0          | 10.0.1      | 10ef44a7  |
| Microsoft.EntityFrameworkCore.Design                  | 2.0.1          | 10.0.1      | 10ef44a7  |
| Microsoft.EntityFrameworkCore.Relational              | 2.1.0          | 10.0.1      | 10ef44a7  |
| Microsoft.EntityFrameworkCore.SqlServer               | 2.0.1          | 10.0.1      | 10ef44a7  |
| Microsoft.EntityFrameworkCore.Tools                   | 2.0.1          | 10.0.1      | 10ef44a7  |
| Microsoft.Extensions.Caching.Abstractions             | 2.1.0          | 10.0.1      | 10ef44a7  |
| Microsoft.Extensions.Caching.Memory                   | 2.1.0          | 10.0.1      | 10ef44a7  |
| Microsoft.Extensions.Configuration                    | 2.1.0          | 10.0.1      | 10ef44a7  |
| Microsoft.Extensions.Configuration.Abstractions       | 2.0.0; 2.1.0   | 10.0.1      | 10ef44a7  |
| Microsoft.Extensions.Configuration.Binder             | 2.1.0          | 10.0.1      | 10ef44a7  |
| Microsoft.Extensions.DependencyInjection              | 2.1.0          | 10.0.1      | 10ef44a7  |
| Microsoft.Extensions.DependencyInjection.Abstractions | 2.0.0; 2.1.0   | 10.0.1      | 10ef44a7  |
| Microsoft.Extensions.FileProviders.Abstractions       | 2.0.0          | 10.0.1      | 10ef44a7  |
| Microsoft.Extensions.Hosting.Abstractions             | 2.0.1          | 10.0.1      | 10ef44a7  |
| Microsoft.Extensions.Identity.Core                    | 2.0.1; 2.1.0   | 10.0.1      | 10ef44a7  |
| Microsoft.Extensions.Identity.Stores                  | 2.0.1; 2.1.0   | 10.0.1      | 10ef44a7  |
| Microsoft.Extensions.Logging                          | 2.0.0; 2.1.0   | 10.0.1      | 10ef44a7  |
| Microsoft.Extensions.Logging.Abstractions             | 2.0.0; 2.1.0   | 10.0.1      | 10ef44a7  |
| Microsoft.Extensions.ObjectPool                       | 2.0.0          | 10.0.1      | 10ef44a7  |
| Microsoft.Extensions.Options                          | 2.0.0; 2.1.0   | 10.0.1      | 10ef44a7  |
| Microsoft.Extensions.Primitives                       | 2.0.0; 2.1.0   | 10.0.1      | 10ef44a7  |
| Microsoft.Extensions.WebEncoders                      | 2.0.0          | 10.0.1      | 10ef44a7  |
| Microsoft.Net.Http.Headers                            | 2.0.1          | 10.0.1      | 10ef44a7  |
| Microsoft.VisualStudio.Web.CodeGeneration.Design      | 2.0.1          | 10.0.1      | 10ef44a7  |
| System.Collections.Immutable                          | 1.5.0          | 10.0.1      | 10ef44a7  |
| System.Data.SqlClient                                 | 4.4.0          | 4.9.0       | 10ef44a7  |
| System.Diagnostics.DiagnosticSource                   | 4.5.0          | 10.0.1      | 10ef44a7  |
| System.Runtime.CompilerServices.Unsafe                | 4.4.0; 4.5.0   | 6.1.2       | 10ef44a7  |
| System.Security.AccessControl                         | 4.4.0          | 6.0.1       | 10ef44a7  |
| System.Security.Cryptography.Xml                      | 4.4.0          | 10.0.1      | 10ef44a7  |
| System.Text.Encodings.Web                             | 4.4.0          | 10.0.1      | 10ef44a7  |

### Odstraněné balíčky (funkcionalita zahrnuta ve frameworku)

Následující balíčky byly odstraněny, protože jejich funkcionalita je nyní součástí .NET 10:

- Microsoft.AspNetCore
- Microsoft.AspNetCore.Authentication
- Microsoft.AspNetCore.Authentication.Abstractions
- Microsoft.AspNetCore.Authentication.Cookies
- Microsoft.AspNetCore.Authentication.Core
- Microsoft.AspNetCore.Hosting.Abstractions
- Microsoft.AspNetCore.Hosting.Server.Abstractions
- Microsoft.AspNetCore.Http.Abstractions
- Microsoft.AspNetCore.Http.Extensions
- Microsoft.AspNetCore.Mvc
- Microsoft.AspNetCore.Mvc.Razor.ViewCompilation
- Microsoft.AspNetCore.StaticFiles
- Microsoft.VisualStudio.Web.BrowserLink
- Microsoft.Win32.Registry
- System.Buffers
- System.Collections
- System.ComponentModel.Annotations
- System.Diagnostics.Debug
- System.Linq
- System.Linq.Expressions
- System.Linq.Queryable
- System.Memory
- System.Numerics.Vectors
- System.ObjectModel
- System.Reflection
- System.Reflection.Extensions
- System.Runtime
- System.Runtime.Extensions
- System.Security.Principal.Windows
- System.Threading

## All commits

| Commit ID | Description                |
|:----------|:---------------------------|
| 10ef44a7  | Commit upgrade plan        |

## Opravené bezpečnostní zranitelnosti

Během upgradu byly opraveny následující bezpečnostní zranitelnosti:

- **Microsoft.AspNetCore.Http** (2.0.1 → 2.3.0)
- **Microsoft.AspNetCore.Identity** (2.0.1 → 2.3.1)
- **System.Data.SqlClient** (4.4.0 → 4.9.0)
- **System.Security.Cryptography.Xml** (4.4.0 → 10.0.1)
- **System.Text.Encodings.Web** (4.4.0 → 10.0.1)

## Další kroky

1. **Přejděte na moderní ASP.NET Core model**: Aplikace používá starší `Startup.cs` pattern. Zvažte migraci na nový minimal hosting model s `Program.cs`.

2. **Aktualizace konfigurace Identity**: Entity Framework Core 10 může vyžadovat úpravy konfigurace ASP.NET Core Identity.

3. **Testování aplikace**: Důkladně otestujte všechny funkce aplikace, zejména:
   - Autentizaci a autorizaci uživatelů
   - Databázové operace (EF Core)
   - Všechny controllery a views

4. **Breaking changes**: Zkontrolujte oficiální dokumentaci breaking changes pro .NET 10 a ASP.NET Core 10.

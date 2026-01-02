# .NET 10 Upgrade Plan

## Execution Steps

Execute steps below sequentially one by one in the order they are listed.

1. Validate that a .NET 10 SDK required for this upgrade is installed on the machine and if not, help to get it installed.
2. Ensure that the SDK version specified in global.json files is compatible with the .NET 10 upgrade.
3. Upgrade Model\Model.csproj
4. Upgrade Entity\Entity.csproj
5. Upgrade Web\Web.csproj

## Settings

This section contains settings and data used by execution steps.

### Excluded projects

No projects are excluded from this upgrade.

### Aggregate NuGet packages modifications across all projects

NuGet packages used across all selected projects or their dependencies that need version update in projects that reference them.

| Package Name                                           | Current Version      | New Version | Description                                                    |
|:-------------------------------------------------------|:--------------------:|:-----------:|:---------------------------------------------------------------|
| Microsoft.AspNetCore                                   | 2.0.1                |             | Functionality included with framework reference                |
| Microsoft.AspNetCore.Authentication                    | 2.0.1                |             | Deprecated package - functionality included with framework     |
| Microsoft.AspNetCore.Authentication.Abstractions       | 2.0.1                |             | Deprecated package - functionality included with framework     |
| Microsoft.AspNetCore.Authentication.Cookies            | 2.0.1                |             | Functionality included with framework reference                |
| Microsoft.AspNetCore.Authentication.Core               | 2.0.1                |             | Deprecated package - functionality included with framework     |
| Microsoft.AspNetCore.Cryptography.Internal             | 2.0.1; 2.1.0         | 10.0.1      | Recommended for .NET 10                                        |
| Microsoft.AspNetCore.Cryptography.KeyDerivation        | 2.0.1; 2.1.0         | 10.0.1      | Recommended for .NET 10                                        |
| Microsoft.AspNetCore.DataProtection                    | 2.0.1                | 10.0.1      | Recommended for .NET 10                                        |
| Microsoft.AspNetCore.DataProtection.Abstractions       | 2.0.1                | 10.0.1      | Recommended for .NET 10                                        |
| Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore   | 2.0.1                | 10.0.1      | Recommended for .NET 10                                        |
| Microsoft.AspNetCore.Hosting.Abstractions              | 2.0.1                |             | Deprecated package - functionality included with framework     |
| Microsoft.AspNetCore.Hosting.Server.Abstractions       | 2.0.1                |             | Deprecated package - functionality included with framework     |
| Microsoft.AspNetCore.Http                              | 2.0.1                | 2.3.0       | Security vulnerability                                         |
| Microsoft.AspNetCore.Http.Abstractions                 | 2.0.1; 2.0.2         |             | Deprecated package - functionality included with framework     |
| Microsoft.AspNetCore.Http.Extensions                   | 2.0.1                |             | Deprecated package - functionality included with framework     |
| Microsoft.AspNetCore.Http.Features                     | 2.0.1; 2.0.2         | 5.0.17      | Recommended for .NET 10                                        |
| Microsoft.AspNetCore.Identity                          | 2.0.1                | 2.3.1       | Security vulnerability                                         |
| Microsoft.AspNetCore.Identity.EntityFrameworkCore      | 2.0.1                | 10.0.1      | Recommended for .NET 10                                        |
| Microsoft.AspNetCore.Mvc                               | 2.0.1                |             | Functionality included with framework reference                |
| Microsoft.AspNetCore.Mvc.Razor.ViewCompilation         | 2.0.1                |             | Functionality included with framework reference                |
| Microsoft.AspNetCore.StaticFiles                       | 2.0.1                |             | Functionality included with framework reference                |
| Microsoft.AspNetCore.WebUtilities                      | 2.0.1                | 10.0.1      | Recommended for .NET 10                                        |
| Microsoft.CSharp                                       | 4.4.0                | 4.7.0       | Recommended for .NET 10                                        |
| Microsoft.EntityFrameworkCore                          | 2.1.0                | 10.0.1      | Recommended for .NET 10                                        |
| Microsoft.EntityFrameworkCore.Abstractions             | 2.1.0                | 10.0.1      | Recommended for .NET 10                                        |
| Microsoft.EntityFrameworkCore.Analyzers                | 2.1.0                | 10.0.1      | Recommended for .NET 10                                        |
| Microsoft.EntityFrameworkCore.Design                   | 2.0.1                | 10.0.1      | Recommended for .NET 10                                        |
| Microsoft.EntityFrameworkCore.Relational               | 2.1.0                | 10.0.1      | Recommended for .NET 10                                        |
| Microsoft.EntityFrameworkCore.SqlServer                | 2.0.1                | 10.0.1      | Recommended for .NET 10                                        |
| Microsoft.EntityFrameworkCore.Tools                    | 2.0.1                | 10.0.1      | Recommended for .NET 10                                        |
| Microsoft.Extensions.Caching.Abstractions              | 2.1.0                | 10.0.1      | Recommended for .NET 10                                        |
| Microsoft.Extensions.Caching.Memory                    | 2.1.0                | 10.0.1      | Recommended for .NET 10                                        |
| Microsoft.Extensions.Configuration                     | 2.1.0                | 10.0.1      | Recommended for .NET 10                                        |
| Microsoft.Extensions.Configuration.Abstractions        | 2.0.0; 2.1.0         | 10.0.1      | Recommended for .NET 10                                        |
| Microsoft.Extensions.Configuration.Binder              | 2.1.0                | 10.0.1      | Recommended for .NET 10                                        |
| Microsoft.Extensions.DependencyInjection               | 2.1.0                | 10.0.1      | Recommended for .NET 10                                        |
| Microsoft.Extensions.DependencyInjection.Abstractions  | 2.0.0; 2.1.0         | 10.0.1      | Recommended for .NET 10                                        |
| Microsoft.Extensions.FileProviders.Abstractions        | 2.0.0                | 10.0.1      | Recommended for .NET 10                                        |
| Microsoft.Extensions.Hosting.Abstractions              | 2.0.1                | 10.0.1      | Recommended for .NET 10                                        |
| Microsoft.Extensions.Identity.Core                     | 2.0.1; 2.1.0         | 10.0.1      | Recommended for .NET 10                                        |
| Microsoft.Extensions.Identity.Stores                   | 2.0.1; 2.1.0         | 10.0.1      | Recommended for .NET 10                                        |
| Microsoft.Extensions.Logging                           | 2.0.0; 2.1.0         | 10.0.1      | Recommended for .NET 10                                        |
| Microsoft.Extensions.Logging.Abstractions              | 2.0.0; 2.1.0         | 10.0.1      | Recommended for .NET 10                                        |
| Microsoft.Extensions.ObjectPool                        | 2.0.0                | 10.0.1      | Recommended for .NET 10                                        |
| Microsoft.Extensions.Options                           | 2.0.0; 2.1.0         | 10.0.1      | Recommended for .NET 10                                        |
| Microsoft.Extensions.Primitives                        | 2.0.0; 2.1.0         | 10.0.1      | Recommended for .NET 10                                        |
| Microsoft.Extensions.WebEncoders                       | 2.0.0                | 10.0.1      | Recommended for .NET 10                                        |
| Microsoft.Net.Http.Headers                             | 2.0.1                | 10.0.1      | Recommended for .NET 10                                        |
| Microsoft.VisualStudio.Web.BrowserLink                 | 2.0.1                |             | Functionality included with framework reference                |
| Microsoft.VisualStudio.Web.CodeGeneration.Design       | 2.0.1                | 10.0.1      | Recommended for .NET 10                                        |
| Microsoft.Win32.Registry                               | 4.4.0                |             | Functionality included with framework reference                |
| System.Buffers                                         | 4.4.0                |             | Functionality included with framework reference                |
| System.Collections                                     | 4.0.11               |             | Functionality included with framework reference                |
| System.Collections.Immutable                           | 1.5.0                | 10.0.1      | Recommended for .NET 10                                        |
| System.ComponentModel.Annotations                      | 4.4.0; 4.5.0         |             | Functionality included with framework reference                |
| System.Data.SqlClient                                  | 4.4.0                | 4.9.0       | Security vulnerability                                         |
| System.Diagnostics.Debug                               | 4.0.11               |             | Functionality included with framework reference                |
| System.Diagnostics.DiagnosticSource                    | 4.5.0                | 10.0.1      | Recommended for .NET 10                                        |
| System.Linq                                            | 4.1.0                |             | Functionality included with framework reference                |
| System.Linq.Expressions                                | 4.1.0                |             | Functionality included with framework reference                |
| System.Linq.Queryable                                  | 4.0.1                |             | Functionality included with framework reference                |
| System.Memory                                          | 4.5.0                |             | Functionality included with framework reference                |
| System.Numerics.Vectors                                | 4.4.0                |             | Functionality included with framework reference                |
| System.ObjectModel                                     | 4.0.12               |             | Functionality included with framework reference                |
| System.Reflection                                      | 4.1.0                |             | Functionality included with framework reference                |
| System.Reflection.Extensions                           | 4.0.1                |             | Functionality included with framework reference                |
| System.Runtime                                         | 4.1.0                |             | Functionality included with framework reference                |
| System.Runtime.CompilerServices.Unsafe                 | 4.4.0; 4.5.0         | 6.1.2       | Recommended for .NET 10                                        |
| System.Runtime.Extensions                              | 4.1.0                |             | Functionality included with framework reference                |
| System.Security.AccessControl                          | 4.4.0                | 6.0.1       | Recommended for .NET 10                                        |
| System.Security.Cryptography.Xml                       | 4.4.0                | 10.0.1      | Security vulnerability                                         |
| System.Security.Principal.Windows                      | 4.4.0                |             | Functionality included with framework reference                |
| System.Text.Encodings.Web                              | 4.4.0                | 10.0.1      | Security vulnerability                                         |
| System.Threading                                       | 4.0.11               |             | Functionality included with framework reference                |

### Project upgrade details

This section contains details about each project upgrade and modifications that need to be done in the project.

#### Model\Model.csproj modifications

Project properties changes:
- Target framework should be changed from `.NETFramework,Version=v4.8` to `net10.0`
- Project file needs to be converted to SDK-style

NuGet packages changes:
- Microsoft.AspNetCore.Authentication (2.0.1) - deprecated, functionality included with framework
- Microsoft.AspNetCore.Authentication.Abstractions (2.0.1) - deprecated, functionality included with framework
- Microsoft.AspNetCore.Authentication.Cookies (2.0.1) - deprecated, functionality included with framework
- Microsoft.AspNetCore.Authentication.Core (2.0.1) - deprecated, functionality included with framework
- Microsoft.AspNetCore.Cryptography.Internal should be updated from 2.0.1 to 10.0.1
- Microsoft.AspNetCore.Cryptography.KeyDerivation should be updated from 2.0.1 to 10.0.1
- Microsoft.AspNetCore.DataProtection should be updated from 2.0.1 to 10.0.1
- Microsoft.AspNetCore.DataProtection.Abstractions should be updated from 2.0.1 to 10.0.1
- Microsoft.AspNetCore.Hosting.Abstractions (2.0.1) - deprecated, functionality included with framework
- Microsoft.AspNetCore.Hosting.Server.Abstractions (2.0.1) - deprecated, functionality included with framework
- Microsoft.AspNetCore.Http should be updated from 2.0.1 to 2.3.0 (*security vulnerability*)
- Microsoft.AspNetCore.Http.Abstractions (2.0.1) - deprecated, functionality included with framework
- Microsoft.AspNetCore.Http.Extensions (2.0.1) - deprecated, functionality included with framework
- Microsoft.AspNetCore.Http.Features should be updated from 2.0.1 to 5.0.17
- Microsoft.AspNetCore.Identity should be updated from 2.0.1 to 2.3.1 (*security vulnerability*)
- Microsoft.AspNetCore.WebUtilities should be updated from 2.0.1 to 10.0.1
- Microsoft.Extensions.Configuration.Abstractions should be updated from 2.0.0 to 10.0.1
- Microsoft.Extensions.DependencyInjection.Abstractions should be updated from 2.0.0 to 10.0.1
- Microsoft.Extensions.FileProviders.Abstractions should be updated from 2.0.0 to 10.0.1
- Microsoft.Extensions.Hosting.Abstractions should be updated from 2.0.1 to 10.0.1
- Microsoft.Extensions.Identity.Core should be updated from 2.0.1 to 10.0.1
- Microsoft.Extensions.Identity.Stores should be updated from 2.0.1 to 10.0.1
- Microsoft.Extensions.Logging should be updated from 2.0.0 to 10.0.1
- Microsoft.Extensions.Logging.Abstractions should be updated from 2.0.0 to 10.0.1
- Microsoft.Extensions.ObjectPool should be updated from 2.0.0 to 10.0.1
- Microsoft.Extensions.Options should be updated from 2.0.0 to 10.0.1
- Microsoft.Extensions.Primitives should be updated from 2.0.0 to 10.0.1
- Microsoft.Extensions.WebEncoders should be updated from 2.0.0 to 10.0.1
- Microsoft.Net.Http.Headers should be updated from 2.0.1 to 10.0.1
- Microsoft.Win32.Registry (4.4.0) - functionality included with framework reference
- System.Buffers (4.4.0) - functionality included with framework reference
- System.ComponentModel.Annotations (4.4.0) - functionality included with framework reference
- System.Runtime.CompilerServices.Unsafe should be updated from 4.4.0 to 6.1.2
- System.Security.AccessControl should be updated from 4.4.0 to 6.0.1
- System.Security.Cryptography.Xml should be updated from 4.4.0 to 10.0.1 (*security vulnerability*)
- System.Security.Principal.Windows (4.4.0) - functionality included with framework reference
- System.Text.Encodings.Web should be updated from 4.4.0 to 10.0.1 (*security vulnerability*)

#### Entity\Entity.csproj modifications

Project properties changes:
- Target framework should be changed from `.NETFramework,Version=v4.8` to `net10.0`
- Project file needs to be converted to SDK-style

NuGet packages changes:
- Microsoft.AspNetCore.Authentication (2.0.1) - deprecated, functionality included with framework
- Microsoft.AspNetCore.Authentication.Abstractions (2.0.1) - deprecated, functionality included with framework
- Microsoft.AspNetCore.Authentication.Cookies (2.0.1) - deprecated, functionality included with framework
- Microsoft.AspNetCore.Authentication.Core (2.0.1) - deprecated, functionality included with framework
- Microsoft.AspNetCore.Cryptography.Internal should be updated from 2.1.0 to 10.0.1
- Microsoft.AspNetCore.Cryptography.KeyDerivation should be updated from 2.1.0 to 10.0.1
- Microsoft.AspNetCore.DataProtection should be updated from 2.0.1 to 10.0.1
- Microsoft.AspNetCore.DataProtection.Abstractions should be updated from 2.0.1 to 10.0.1
- Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore should be updated from 2.0.1 to 10.0.1
- Microsoft.AspNetCore.Hosting.Abstractions (2.0.1) - deprecated, functionality included with framework
- Microsoft.AspNetCore.Hosting.Server.Abstractions (2.0.1) - deprecated, functionality included with framework
- Microsoft.AspNetCore.Http should be updated from 2.0.1 to 2.3.0 (*security vulnerability*)
- Microsoft.AspNetCore.Http.Abstractions (2.0.2) - deprecated, functionality included with framework
- Microsoft.AspNetCore.Http.Extensions (2.0.1) - deprecated, functionality included with framework
- Microsoft.AspNetCore.Http.Features should be updated from 2.0.2 to 5.0.17
- Microsoft.AspNetCore.Identity should be updated from 2.0.1 to 2.3.1 (*security vulnerability*)
- Microsoft.AspNetCore.Identity.EntityFrameworkCore should be updated from 2.0.1 to 10.0.1
- Microsoft.AspNetCore.WebUtilities should be updated from 2.0.1 to 10.0.1
- Microsoft.CSharp should be updated from 4.4.0 to 4.7.0
- Microsoft.EntityFrameworkCore should be updated from 2.1.0 to 10.0.1
- Microsoft.EntityFrameworkCore.Abstractions should be updated from 2.1.0 to 10.0.1
- Microsoft.EntityFrameworkCore.Analyzers should be updated from 2.1.0 to 10.0.1
- Microsoft.EntityFrameworkCore.Design should be updated from 2.0.1 to 10.0.1
- Microsoft.EntityFrameworkCore.Relational should be updated from 2.1.0 to 10.0.1
- Microsoft.EntityFrameworkCore.SqlServer should be updated from 2.0.1 to 10.0.1
- Microsoft.Extensions.Caching.Abstractions should be updated from 2.1.0 to 10.0.1
- Microsoft.Extensions.Caching.Memory should be updated from 2.1.0 to 10.0.1
- Microsoft.Extensions.Configuration should be updated from 2.1.0 to 10.0.1
- Microsoft.Extensions.Configuration.Abstractions should be updated from 2.1.0 to 10.0.1
- Microsoft.Extensions.Configuration.Binder should be updated from 2.1.0 to 10.0.1
- Microsoft.Extensions.DependencyInjection should be updated from 2.1.0 to 10.0.1
- Microsoft.Extensions.DependencyInjection.Abstractions should be updated from 2.1.0 to 10.0.1
- Microsoft.Extensions.FileProviders.Abstractions should be updated from 2.0.0 to 10.0.1
- Microsoft.Extensions.Hosting.Abstractions should be updated from 2.0.1 to 10.0.1
- Microsoft.Extensions.Identity.Core should be updated from 2.1.0 to 10.0.1
- Microsoft.Extensions.Identity.Stores should be updated from 2.1.0 to 10.0.1
- Microsoft.Extensions.Logging should be updated from 2.1.0 to 10.0.1
- Microsoft.Extensions.Logging.Abstractions should be updated from 2.1.0 to 10.0.1
- Microsoft.Extensions.ObjectPool should be updated from 2.0.0 to 10.0.1
- Microsoft.Extensions.Options should be updated from 2.1.0 to 10.0.1
- Microsoft.Extensions.Primitives should be updated from 2.1.0 to 10.0.1
- Microsoft.Extensions.WebEncoders should be updated from 2.0.0 to 10.0.1
- Microsoft.Net.Http.Headers should be updated from 2.0.1 to 10.0.1
- Microsoft.Win32.Registry (4.4.0) - functionality included with framework reference
- System.Buffers (4.4.0) - functionality included with framework reference
- System.Collections (4.0.11) - functionality included with framework reference
- System.Collections.Immutable should be updated from 1.5.0 to 10.0.1
- System.ComponentModel.Annotations (4.5.0) - functionality included with framework reference
- System.Data.SqlClient should be updated from 4.4.0 to 4.9.0 (*security vulnerability*)
- System.Diagnostics.Debug (4.0.11) - functionality included with framework reference
- System.Diagnostics.DiagnosticSource should be updated from 4.5.0 to 10.0.1
- System.Linq (4.1.0) - functionality included with framework reference
- System.Linq.Expressions (4.1.0) - functionality included with framework reference
- System.Linq.Queryable (4.0.1) - functionality included with framework reference
- System.Memory (4.5.0) - functionality included with framework reference
- System.Numerics.Vectors (4.4.0) - functionality included with framework reference
- System.ObjectModel (4.0.12) - functionality included with framework reference
- System.Reflection (4.1.0) - functionality included with framework reference
- System.Reflection.Extensions (4.0.1) - functionality included with framework reference
- System.Runtime (4.1.0) - functionality included with framework reference
- System.Runtime.CompilerServices.Unsafe should be updated from 4.5.0 to 6.1.2
- System.Runtime.Extensions (4.1.0) - functionality included with framework reference
- System.Security.AccessControl should be updated from 4.4.0 to 6.0.1
- System.Security.Cryptography.Xml should be updated from 4.4.0 to 10.0.1 (*security vulnerability*)
- System.Security.Principal.Windows (4.4.0) - functionality included with framework reference
- System.Text.Encodings.Web should be updated from 4.4.0 to 10.0.1 (*security vulnerability*)
- System.Threading (4.0.11) - functionality included with framework reference

#### Web\Web.csproj modifications

Project properties changes:
- Target framework should be changed from `net48` to `net10.0`

NuGet packages changes:
- Microsoft.AspNetCore (2.0.1) - functionality included with framework reference
- Microsoft.AspNetCore.Authentication.Cookies (2.0.1) - functionality included with framework reference
- Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore should be updated from 2.0.1 to 10.0.1
- Microsoft.AspNetCore.Identity.EntityFrameworkCore should be updated from 2.0.1 to 10.0.1
- Microsoft.AspNetCore.Mvc (2.0.1) - functionality included with framework reference
- Microsoft.AspNetCore.Mvc.Razor.ViewCompilation (2.0.1) - functionality included with framework reference
- Microsoft.AspNetCore.StaticFiles (2.0.1) - functionality included with framework reference
- Microsoft.EntityFrameworkCore should be updated from 2.1.0 to 10.0.1
- Microsoft.EntityFrameworkCore.Design should be updated from 2.0.1 to 10.0.1
- Microsoft.EntityFrameworkCore.Relational should be updated from 2.1.0 to 10.0.1
- Microsoft.EntityFrameworkCore.SqlServer should be updated from 2.0.1 to 10.0.1
- Microsoft.EntityFrameworkCore.Tools should be updated from 2.0.1 to 10.0.1
- Microsoft.Extensions.Identity.Stores should be updated from 2.1.0 to 10.0.1
- Microsoft.VisualStudio.Web.BrowserLink (2.0.1) - functionality included with framework reference
- Microsoft.VisualStudio.Web.CodeGeneration.Design should be updated from 2.0.1 to 10.0.1

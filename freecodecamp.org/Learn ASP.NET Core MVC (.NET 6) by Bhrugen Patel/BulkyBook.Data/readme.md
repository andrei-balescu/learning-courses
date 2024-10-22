# BulkBook data prokect
Using Entity Framework with SQL Server

## EF code-first migrations

Install required tools: 
- create manifest inside _project root_: `dotnet new tool-manifest`  
- Install migration tool in local project: `dotnet tool install --local dotnet-ef --version 6`  
- To install tool globally: `dotnet tool install --global dotnet-ef --version 6`  
Add EF design pagkage to `BulkyBook` project: `dotnet add package Microsoft.EntityFrameworkCore.Design -v 6`  

Add migration to project: `dotnet ef --startup-project ../BulkyBook migrations add AddCategoryToDatabase`  
Update DB: `dotnet ef --startup-project ../BulkyBook database update` (will also create DB if not exists)  
To unapply and remove migration: `dotnet ef --startup-project ../BulkyBook migrations remove --force`  
**NOTE**: Change `BulkyBook` connnection string in `appSettings.Development.json` to point to local machine
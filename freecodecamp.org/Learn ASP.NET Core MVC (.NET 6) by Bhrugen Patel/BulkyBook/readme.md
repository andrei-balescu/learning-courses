# BulkyBook web app

## EF code-first migrations

Install required tools: 
- create manifest inside project: `dotnet new tool-manifest`  
- Install migration tool in local project: `dotnet tool install --local dotnet-ef --version 6`  
- To install tool globally: `dotnet tool install --global dotnet-ef --version 6`  
Add EF design pagkage to project: `dotnet add package Microsoft.EntityFrameworkCore.Design -v 6`  

Add migration to project: `dotnet ef migrations add AddCategoryToDatabase`  
Update DB: `dotnet ef database update` (will also create DB if not exists)  
To unapply and remove migration: `dotnet ef migrations remove --force`

## Debugging with Docker
**Configuring debugger**
Ctrl+Shift+P -> Docker: Add Docker Files to Workspace -> Add optional docker-compose  
Run and Debug -> Add Configuration -> Docker .NET Attach  

**Running debugger**
Start container in Debug mode: `docker-compose -f docker-compose.debug.yml up --build -d`  
Run and Debug -> Docker .NET Attach -> select container

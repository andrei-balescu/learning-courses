# BulkyBook web app

## EF code-first migrations
Install required tools: `dotnet tool install --global dotnet-ef --version 6`  
Add EF design pagkage to project: `dotnet add package Microsoft.EntityFrameworkCore.Design -v 6`  
Add migration to project: `dotnet-ef migrations add AddCategoryToDatabase`  
Update DB: `dotnet-ef database update` (will also create DB if not exists)  
To unapply and remove migration: `dotnet-ef migrations remove --force`

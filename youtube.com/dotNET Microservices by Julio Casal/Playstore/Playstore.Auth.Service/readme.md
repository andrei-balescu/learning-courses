# Playstore.Auth.Service project

.NET 6 authentication / authorization API

## Set up DB for ASP.NET Core identity

Install tools:  
- Create manifest inside _project root_: `dotnet new tool-manifest`
- Install migration tool in local project: `dotnet tool install --local dotnet-ef --version 6`
- Add EF design pagkage to project: `dotnet add package Microsoft.EntityFrameworkCore.Design -v 6`

Create identity tables:  
- Update DB: `dotnet ef database update` (will also create DB if not exists)  
- To unapply and remove migration: `dotnet ef migrations remove --force` (some migrations are customized - beware of loss of functionality)
**NOTE**: Change connnection string in `appSettings.Development.json` to point to local machine

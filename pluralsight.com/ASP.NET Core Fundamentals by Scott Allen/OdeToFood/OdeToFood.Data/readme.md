# Ode to Dood - data management library

## Using Entity Framework migrations

* Use the following command to get info about the EF provider: `dotnet ef dbcontext info --startup-project=../OdeToFood`
* Use the following command to create a DB migration: `dotnet ef migrations add initialcreate -s ../OdeToFood`
* Use the following command to update DB using a created DB migration: `dotnet ef database update -s ../OdeToFood`
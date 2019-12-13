# Ode to Food - data management library

## Setting up Postgres on Ubuntu 18.4

1. Install Postgres and pgAdmin: `sudo apt-get install postgresql pgadmin3`
2. Create new Postgress role: `sudo -u postgres createuser --interactive`
3. Log in to postgres account: `sudo -u postgres psql`
4. Set password for new role: `\password "pgAdmin"`
5. Quit Postgres console: `\q`
5. Log in to role in pgAdmin client to manage server

## Using Entity Framework migrations

* Use the following command to get info about the EF provider: `dotnet ef dbcontext info --startup-project=../OdeToFood`
* Use the following command to get info about the DB contexts configured in the project: `dotnet ef dbcontext list --startup-project=../OdeToFood`
* Use the following command to create a DB migration: `dotnet ef migrations add initialcreate -s ../OdeToFood`
* Use the following command to update DB using a created DB migration: `dotnet ef database update -s ../OdeToFood`
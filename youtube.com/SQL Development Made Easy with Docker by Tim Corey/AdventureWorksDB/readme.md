# Docker setup for SQL server

Run following to create new image on the fly: `docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Pwd12345!" -p 11433:1433 -d mcr.microsoft.com/mssql/server:2022-latest`

Run folowing to build from dockerfile: 
- Build: `docker build -t mssql-aw:latest .`
- Run: `docker run -p 11433:1433 -d mssql-aw:latest`

Connect to:`localhost,11433`

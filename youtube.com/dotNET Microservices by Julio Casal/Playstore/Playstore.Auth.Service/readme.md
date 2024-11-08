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

Decode token information: https://jwt.io/


## Endpoints
### /auth

Register a user:  
Request: **POST** /auth/register
``` json
POST /auth/register
{
  "name": "User1",
  "password": "Test1!",
  "role": 2
}
```
Response:
``` json
{
  "id": "07be52d9-6097-4e51-85a2-73d297ac22f4",
  "name": "User1"
}
```

---

Login a user:  
Request: **POST** /auth/login
``` json
POST /auth/login
{
  "name": "User1",
  "password": "Test1!",
}
```
Response:
``` json
{
  "user": {
    "id": "07be52d9-6097-4e51-85a2-73d297ac22f4",
    "name": "User1"
  },
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJUZXN0VXNlcjE1Iiwic3ViIjoiMDdiZTUyZDktNjA5Ny00ZTUxLTg1YTItNzNkMjk3YWMyMmY0IiwianRpIjoiN2U3OWMxMzctOWFmZi00MDk4LTlmZjMtODFlNzAxNjAyZGU4Iiwicm9sZSI6IlBsYXllciIsIm5iZiI6MTczMTA3NTM5MiwiZXhwIjoxNzMxMDc4OTkyLCJpYXQiOjE3MzEwNzUzOTIsImlzcyI6IlBsYXlzdG9yZS5BdXRoLlNlcnZpY2UiLCJhdWQiOiJQbGF5c3RvcmUuQ2xpZW50In0.G_dE7rLPn4PWhi7HP-9ZNE2V4QZftMnaVHr0Kox3rE4"
}
```
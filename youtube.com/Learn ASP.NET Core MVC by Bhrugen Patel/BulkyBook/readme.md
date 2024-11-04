# BulkyBook web app

## Debugging with Docker
### Configuring debugger
Ctrl+Shift+P -> Docker: Add Docker Files to Workspace -> Add optional docker-compose  
Run and Debug -> Add Configuration -> Docker .NET Attach  

### Running debugger
Start container in Debug mode: `docker-compose -f docker-compose.debug.yml up --build -d`  
Run and Debug -> Docker .NET Attach -> select container

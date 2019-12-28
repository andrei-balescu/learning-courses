# Basic ASP.NET Core website with PostgreSQL support

## Communicate with PostgreSQL via bridge network
* Create bridge network: `docker network create --driver bridge odetofood`
    * `--driver` specifies what kind of network to create; defaults to `bridge`
* Run postgres container inside network: `docker run -d --net odetofood --name postgresql -e POSTGRES_PASSWORD=password postgres`
    * `--net` specifies which network to run in
    * `--name` name assigned to container inside network; other containers can access it via the name
    * no ports exposed
### VS Code images
* Debug application inside network: `F5`
    * Make sure correct launch target is selected: `Docker .NET Core Launch`
* Run application inside network: `F1` > `Tasks: Run Task` > `docker-run:release`
* Go to `https://localhost:5001` in browser
### Autonomous images
* Build image: `docker build -t andrei.balescu/odetofood .`
* Run container `docker run -d -p 5002:443 --net odetofood --name otf andrei.balescu/odetofood`
* Go to `https://localhost:5002` in browser
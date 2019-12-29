# Basic ASP.NET Core website with PostgreSQL support

## Communicate with PostgreSQL via bridge network
* Create bridge network: `docker network create --driver bridge odetofood`
    * `--driver` specifies what kind of network to create; defaults to `bridge`
* Run postgres container inside network: `docker run -d --net odetofood --name postgresql -e POSTGRES_PASSWORD=password postgres:12`
    * `--net` specifies which network to run in
    * `--name` name assigned to container inside network; other containers can access it via the name
    * no ports exposed
* Restart container once stopped: `docker start postgresql`
### VS Code images
* Debug application inside network: `F5`
    * Make sure correct launch target is selected: `Docker .NET Core Launch`
* Run application inside network: `F1` > `Tasks: Run Task` > `docker-run:release`
* Go to `https://localhost:5002` in browser
### Manual images
* Build image: `docker build -t odetofood:manual .`
* Run container `docker run -d -p 5003:443 --net odetofood --name odetofood-manual odetofood:manual`
* Go to `https://localhost:5003` in browser
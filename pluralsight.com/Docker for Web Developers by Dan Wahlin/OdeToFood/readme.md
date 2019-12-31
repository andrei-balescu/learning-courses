# Basic ASP.NET Core website with PostgreSQL support

## Communicate with PostgreSQL via bridge network
* Create bridge network: `docker network create --driver bridge odetofood`
    * `--driver` specifies what kind of network to create; defaults to `bridge`
* Run postgres container inside network: `docker run -d --net odetofood --name postgresql -e POSTGRES_PASSWORD=password postgres:12`
    * `--net` specifies which network to run in
    * `--name` name assigned to container inside network; other containers can access it via the name
    * no ports exposed
* Restart container once stopped: `docker start postgresql`
## VS Code images
* Debug application inside network: `F5`
    * Make sure correct launch target is selected: `Docker .NET Core Launch`
* Go to `http://localhost:5000` or `http://localhost:5001` in browser
* Run application inside network: `F1` > `Tasks: Run Task` > `docker-run:release`
* Go to `https://localhost:5002` in browser
## Manual images
* Build image: `docker build -t odetofood:manual .`
* Run container `docker run -d -p 5003:443 --net odetofood --name odetofood-manual odetofood:manual`
* Go to `https://localhost:5003` in browser
## Using docker compose
* Build & run services: `docker-compose up -d`
    * `-d` run in background
* Go to `https://localhost:5004` in browser
* Build only: `docker-compose build`
* Stop running services: `docker-compose stop`
* Restart stopped services: `docker-compose start`
* Stop and remove services: `docker-compose down`
### Multi-service compose file
* Run services: `docker-compose -f docker-compose-multi-service.yml up -d`
* Go to `http://localhost:5005` in browser to access first service
* Stop services: `docker-compose -f docker-compose-multi-service.yml stop`
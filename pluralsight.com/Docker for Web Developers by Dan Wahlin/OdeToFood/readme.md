# Basic ASP.NET Core website with PostgreSQL support

## Communicate with PostgreSQL via bridge network
* Create bridge network: `docker network create --driver bridge odetofood`
    * `--driver` specifies what kind of network to create; defaults to `bridge`
* Run postgres container inside network: `docker run -d --net odetofood --name postgresql -e POSTGRES_PASSWORD=password postgres`
    * `--net` specifies which network to run in
    * `--name` name assigned to container inside network; other containers can access it via the name
    * no ports exposed
* Debug application inside network: `F5`
    * Make sure to run docker launch target
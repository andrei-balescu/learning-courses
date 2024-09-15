# Global
Run `composer update -d app` to set up package dependencies.
Commit `app/composer.lock` to git

Start project: `docker-compose up` or `docker-compose up -d` (detach)
Start project (production environment): ` docker-compose -f docker-compose.prod.yaml up --build -d`
Go to `http://localhost:8000`
Run `docker ps` or `docker-compose ps` to check status of running containers

# Nginx
Run `docker exec -it docker-php_web_1 bash` to run bash inside nginx container
`cat /etc/nginx/conf.d/default.conf` to check nginx server configuration. To be replaced with `nginx/conf.d/default.conf`

# Debugging
Make sure you have installed PHP Extention pack in VS code to use XDebug
Run -> Add Configuration -> PHP to add debugging configuration to the project
Add following configuration to `.vscode/launch.json` to use XDebug with Docker:
>   {
>       "name": "Listen on Docker for Xdebug",
>       "type": "php",
>       "request": "launch",
>       "port": 9003,
>       "pathMappings": {
>           "/var/www/app": "${workspaceFolder}/app"
>       }
>   },
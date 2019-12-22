# Runnig Node.js website in a Docker container from a volume
## Setting up a Node.js Express sample website 
* Install Node.js express site generator: `sudo apt install node-express-generator`
* Generate Express site using handlebars: `express ExpressSite --hbs`
* Download site dependencies: `cd ExpressSite/ && npm install`
* Run site: `npm start`
* To access site go to `localhost:3000` in browser

## Setting up website as a volume in a Node.JS Docker image
Download Node.js image and create container: `docker run -p 8080:3000 -v "$(pwd)":/var/www -w "/var/www" node npm start`
* `-p` maps ports host:container
* `-v` specifies volume. Use "" if directory names have spaces
* `-w` specifies working directory inside container
* `npm start` will run inside the container once started

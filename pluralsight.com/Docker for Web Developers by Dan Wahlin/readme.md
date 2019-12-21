# Docker for Web Developers
## by Dan Wahlin

Building web apps that run the same in multiple environments can be a time-consuming process. This course, Docker for Web Developers, will teach you how to use Docker's open platform so that you can efficiently build apps that run consistently across any machine. First, you'll learn about Docker, its toolbox, the Docker Machine and Docker Client commands, and how all these components help you in your development environment. Next, you'll learn to work with images, as well as Docker containers and how to link and manage them. After that, you'll discover how to get a fully-functional development environment up and running, both locally and in the cloud. By the end of this course, you'll be able to increase your productivity and create lightweight apps that run the same no matter the environment.

### Runnig Node.js website in a Docker container from a volume
## Setting up a Node.js Express sample website 
Install Node.js express site generator: `sudo apt install node-express-generator`
Generate Express site using handlebars: `express ExpressSite --hbs`
Download site dependencies: `cd ExpressSite/ && npm install`
Run site: `npm start`
To access site go to `localhost:3000` in browser

### Setting up website as a volume in a Node.JS Docker image
Download Node.js image and create container: `docker run -p 8080:3000 -v "$(pwd)":/var/www -w "/var/www" node npm start`
* `-p` maps ports host:container
* `-v` specifies volume. Use "" if directory names have spaces
* `-w` specifies working directory inside container
* `npm start` will run inside the container once started
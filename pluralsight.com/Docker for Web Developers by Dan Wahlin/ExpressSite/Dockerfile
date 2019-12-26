# image[:version] to use as a base for creating custom image
FROM node:latest

# can be name / email
# field depdecated in facor of LABEL
# MAINTAINER Andrei Balescu
LABEL "maintainer" = "Andrei Balescu"

# set environment variables inside container
ENV NODE_ENV=production
# optional - port 3000 is default for node.js
ENV PORT=3000

# copy code from current directory to folder inside image
COPY        . /var/www

# directory to run entry comand into
WORKDIR     /var/www

# specify volumes to attach
# replaces contents of the folder inside container with the contents of the mounted folder
# VOLUME      ["/var/www", "/logs"]

# command to run before starting application
RUN         npm install

# default port to expose inside the container; can be overridden through `docker run`
# EXPOSE 3000
EXPOSE $PORT

# command to run to launch application once container running
# `start` command defined in package.json
# can also use ledd preferred `ENTRYPOINT npm start`
ENTRYPOINT ["npm", "start"]
# Configuration for production environment

FROM nginx:1.27.1

# overwrite nginx default.conf inside container with the one in our project
COPY ./nginx/conf.d/default.conf /etc/nginx/conf.d/default.conf
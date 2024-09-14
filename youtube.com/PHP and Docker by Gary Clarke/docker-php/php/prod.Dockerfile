# use minimalist PHP image
FROM php:8.1-fpm-alpine

# install PHP extenstions inside image
RUN docker-php-ext-install pdo pdo_mysql

# copy project files inside container
COPY ./app/public /var/www/html/public
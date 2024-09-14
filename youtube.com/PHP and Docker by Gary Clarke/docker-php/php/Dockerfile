# use minimalist PHP image
FROM php:8.1-fpm-alpine AS app_base

# install PHP extenstions inside image
RUN docker-php-ext-install pdo pdo_mysql

# ---
# Configuration for production environment
# ---
FROM app_base AS app_prod

# copy project files inside container
COPY ./app /var/www
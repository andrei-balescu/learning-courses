# use minimalist PHP image
FROM php:8.1-fpm-alpine AS app_base

# Useful PHP extension installer image, copy binary into your container
COPY --from=mlocati/php-extension-installer /usr/bin/install-php-extensions /usr/local/bin/

# install PHP extenstions inside image
# RUN docker-php-ext-install pdo pdo_mysql
RUN set -eux; \
	install-php-extensions pdo pdo_mysql



# ----------------------------------------
# Configuration for production environment
# ----------------------------------------
FROM app_base AS app_prod

# allow super user - set this if you use Composer as a
# super user at all times like in docker containers
ENV COMPOSER_ALLOW_SUPERUSER=1

# obtain composer using multi-stage build
COPY --from=composer:2.2 /usr/bin/composer /usr/bin/composer

# Here, we are copying only composer.json and composer.lock (instead of copying the entire source)
# right before doing composer install.
# This is enough to take advantage of docker cache and composer install will
# be executed only when composer.json or composer.lock have indeed changed!-
# https://medium.com/@softius/faster-docker-builds-with-composer-install-b4d2b15d0fff
COPY ./app/composer.* ./app/


# install
RUN composer install -d app/ --prefer-dist --no-dev --no-scripts --no-progress --no-interaction

# copy project files inside container
# excluding `vendor` directory (see `.dockerignore`)
COPY ./app /var/www/html/app

# run composer dump-autoload --optimize
RUN composer dump-autoload -d app/ --optimize



# --------------------------
# Cnfiguration for debugging
# --------------------------
FROM app_base AS app_debug

# Copy xdebug config file into container
COPY ./environment/php/conf.d/xdebug.ini /usr/local/etc/php/conf.d/xdebug.ini

# Install xdebug
RUN set -eux; \
	install-php-extensions xdebug
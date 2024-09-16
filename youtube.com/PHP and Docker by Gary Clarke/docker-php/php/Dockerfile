# use minimalist PHP image
FROM php:8.1-fpm-alpine AS app_base

# Useful PHP extension installer image, copy binary into your container
COPY --from=mlocati/php-extension-installer /usr/bin/install-php-extensions /usr/local/bin/

# install PHP extenstions inside image
# RUN docker-php-ext-install pdo pdo_mysql
RUN set -eux; \
	install-php-extensions pdo pdo_mysql

# ---
# Configuration for production environment
# ---
FROM app_base AS app_prod

# copy project files inside container
COPY ./app /var/www/app



# ---
# Cnfiguration for debugging
# ---
FROM app_base AS app_debug

# Copy xdebug config file into container
COPY ./php/conf.d/xdebug.ini /usr/local/etc/php/conf.d/xdebug.ini

# Install xdebug
RUN set -eux; \
	install-php-extensions xdebug
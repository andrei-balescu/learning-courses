# build client app
FROM node:13-alpine AS client
WORKDIR /src
COPY OdeToFood.Client .
ENV OUTPUT_DIR="/app"
RUN npm install && npm run prod

FROM nginx:1-alpine
WORKDIR /etc/nginx/conf.d
RUN rm default.conf
COPY docker-compose-multi-service/nginx/nginx.conf ./default.conf
# copy client app
WORKDIR /var/www
COPY --from=client /app .
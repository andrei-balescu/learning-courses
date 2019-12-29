FROM nginx:1-alpine
WORKDIR /etc/nginx/conf.d
RUN rm default.conf
COPY docker-compose-multi-service/nginx/nginx.conf ./default.conf
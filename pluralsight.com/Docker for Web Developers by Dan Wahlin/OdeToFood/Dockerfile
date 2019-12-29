FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80/tcp 443/tcp

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["OdeToFood/OdeToFood.csproj", "OdeToFood/"]
RUN dotnet restore "OdeToFood/OdeToFood.csproj"
COPY . .
WORKDIR /src/OdeToFood
RUN dotnet build "OdeToFood.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OdeToFood.csproj" -c Release -o /app/publish

FROM base AS release
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OdeToFood.dll"]

# build client app
FROM node:13-alpine AS client
WORKDIR /src
COPY OdeToFood.Client .
ENV OUTPUT_DIR="/app"
RUN npm install && npm run prod

# add self signed SSL certificate for HTTPS communication
FROM build AS https
WORKDIR /app/https
RUN dotnet dev-certs https -ep odetofood.pfx -p password

# create autonomous image
FROM release as final
LABEL author="Andrei Balescu"
# set up HTTPS communication
WORKDIR /root/.aspnet/https
COPY --from=https /app/https .
ENV ASPNETCORE_Kestrel__Certificates__Default__Password="password"
ENV ASPNETCORE_Kestrel__Certificates__Default__Path="/root/.aspnet/https/odetofood.pfx"
ENV ASPNETCORE_URLS="https://+:443"
# copy client app
WORKDIR /app/wwwroot
RUN rm -r *
COPY --from=client /app .
WORKDIR /app
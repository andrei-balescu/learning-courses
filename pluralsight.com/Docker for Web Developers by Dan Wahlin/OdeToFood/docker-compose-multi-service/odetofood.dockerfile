FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80/tcp

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["OdeToFood/OdeToFood.csproj", "OdeToFood/"]
RUN dotnet restore "OdeToFood/OdeToFood.csproj"
COPY . .
WORKDIR /src/OdeToFood
# remove client files - will be served by nginx
RUN rm -r wwwroot
RUN dotnet build "OdeToFood.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OdeToFood.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS="http://+:80"
ENTRYPOINT ["dotnet", "OdeToFood.dll"]
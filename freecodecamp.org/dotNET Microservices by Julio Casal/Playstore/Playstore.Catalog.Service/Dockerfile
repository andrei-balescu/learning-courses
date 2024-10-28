FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5001

# Disable HTTPS redirect - cannot trust certificates in Linux
ENV ASPNETCORE_URLS=http://+:5001

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["Playstore.Catalog.Service/Playstore.Catalog.Service.csproj", "Playstore.Catalog.Service/"]
RUN dotnet restore "Playstore.Catalog.Service/Playstore.Catalog.Service.csproj"
COPY . .
WORKDIR "/src/Playstore.Catalog.Service"
RUN dotnet build "Playstore.Catalog.Service.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Playstore.Catalog.Service.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Playstore.Catalog.Service.dll"]

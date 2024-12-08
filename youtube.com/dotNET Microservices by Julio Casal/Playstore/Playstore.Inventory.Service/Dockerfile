FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5002

# Disable HTTPS redirect - cannot trust certificates in Linux
ENV ASPNETCORE_URLS=http://+:5002

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["Playstore.Inventory.Service/Playstore.Inventory.Service.csproj", "Playstore.Inventory.Service/"]
RUN dotnet restore "Playstore.Inventory.Service/Playstore.Inventory.Service.csproj"
COPY . .
WORKDIR "/src/Playstore.Inventory.Service"
RUN dotnet build "Playstore.Inventory.Service.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Playstore.Inventory.Service.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Playstore.Inventory.Service.dll"]

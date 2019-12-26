FROM mcr.microsoft.com/dotnet/core/sdk:3.1

LABEL author="Andrei Balescu"

# restart kestrel on file change
ENV DOTNET_USE_POLLING_FILE_WATCHER=1
ENV ASPNETCORE_URLS="https://*:5001"

WORKDIR /app

# run application in a bash terminal with file watcher
CMD ["/bin/bash", "-c", "dotnet restore && dotnet watch run"]
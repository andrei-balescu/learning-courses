# create build image from ASP.NET Core sdk image with the alias `publish`
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as publish
WORKDIR /publish
# copy .csproj file to local working directory
COPY ASP.NETCoreDemos.csproj .
# restore dependencies
RUN dotnet restore
# copy remaining source files to working directory
COPY . .
# publish to /publish/out
RUN dotnet dev-certs https -ep /https/sslcertificate.pfx -p cert-password
# generate a self signed SSL certificate
RUN dotnet publish --output ./out

# create production image from ASP.NET Core build image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
LABEL author="Andrei Balescu"
WORKDIR /var/www/aspnetcoreapp
# copy publish output from previous image to local working directory
COPY --from=publish /publish/out .
# copy ssl certificate from publish image
COPY --from=publish /https /https
# settings for listening on port 5001
ENV ASPNETCORE_URLS=https://*:5001
# settings for loading SSL certificate
ENV ASPNETCORE_HTTPS_PORT=5001
ENV ASPNETCORE_Kestrel__Certificates__Default__Path="/https/sslcertificate.pfx"
ENV ASPNETCORE_Kestrel__Certificates__Default__Password="cert-password"
EXPOSE 5001
# run website
ENTRYPOINT ["dotnet", "ASP.NETCoreDemos.dll"]
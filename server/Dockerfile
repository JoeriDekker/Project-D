# Use the official ASP.NET Core SDK as the base image for building the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /server

# Copy csproj and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Use the official ASP.NET Core runtime as the base image for running the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /server/out .

# Expose the port that your ASP.NET application listens on
EXPOSE 80

# Set the entry point for the container
ENTRYPOINT ["dotnet", "server.dll"]
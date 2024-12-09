# Use the official .NET SDK image to build and publish the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Set the working directory inside the container
WORKDIR /app

# Copy the .csproj file and restore dependencies
COPY *.csproj .
RUN dotnet restore

# Copy the rest of the application files
COPY . .

# Build the application in Release mode
RUN dotnet publish -c Release -o /out

# Use the official ASP.NET Core runtime image for deployment
FROM mcr.microsoft.com/dotnet/aspnet:9.0

# Set the working directory inside the container
WORKDIR /app

# Copy the published output from the build stage
COPY --from=build /out .

# Expose the port on which the application will run
EXPOSE 80

# Set the entry point to run the application
ENTRYPOINT ["dotnet", "HealthTrack.Web.dll"]

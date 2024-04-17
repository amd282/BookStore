# Use the official .NET 6.0 SDK image as the build environment
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy the .csproj file and restore any dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the project files
COPY . ./

# Build the application
RUN dotnet publish -c Release -o out

# Use the official .NET 6.0 runtime image as the base image
FROM mcr.microsoft.com/dotnet/aspnet:6.0

# Set the working directory
WORKDIR /app

# Copy the build output from the build environment
COPY --from=build-env /app/out .

# Define the command to run the application
ENTRYPOINT ["dotnet", "BookStore.dll"]

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

# Expose port 80
EXPOSE 80

# Define the command to run the application
ENV ConnectionStrings__BookStoreContext="mongodb+srv://arafa282:Asmara2024@cluster0.yjdkcv7.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0"
ENTRYPOINT ["dotnet", "BookStore.dll"]

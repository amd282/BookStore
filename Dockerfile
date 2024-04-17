# Use the official image as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:5.0

# Set the working directory in the container
WORKDIR /app

# Copy the build output to the working directory in the container
COPY ./bin/Release/net5.0/publish/ .

# Expose port 80 for the application
EXPOSE 80

# Define the command to run the application
ENTRYPOINT ["dotnet", "BookStore.dll"]

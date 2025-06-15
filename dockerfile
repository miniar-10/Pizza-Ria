# TODO: 1. Use the .NET SDK image to build the app 
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# TODO: 2. Copy only the Backend project files and restore
COPY /Backend/*.csproj /Backend/
WORKDIR /Backend
RUN dotnet restore

# TODO: 3. Copy the entire repo (to get sources, not just csproj)
WORKDIR /src
COPY . . 

# TODO: 4. Build and publish only the Backend project
WORKDIR /src/Backend
RUN dotnet publish -c Release -o /app/publish

# TODO: 5. Use the ASP.NET runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# TODO:6. Expose the port that my app runs on 
EXPOSE 8080

#TODO:7. Add wait for it so the container waits for the db service to be up 
COPY wait-for-it.sh /wait-for-it.sh

# TODO: 8. Run the app 
ENTRYPOINT ["/wait-for-it.sh", "db:5432", "--","dotnet", "Backend.dll"]

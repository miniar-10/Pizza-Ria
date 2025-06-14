# TODO: 1. Use the .NET SDK image to build the app 
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
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
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/publish .

# TODO:6. Expose the port that my app runs on 
EXPOSE 80

# TODO: 7. Run the app 
ENTRYPOINT ["dotnet", "Backend.dll"]

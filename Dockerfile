# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY src/Api/*.csproj ./src/Api/
COPY src/Application/*.csproj ./src/Application/
COPY src/Domain/*.csproj ./src/Domain/
COPY src/FluentDocker/*.csproj ./src/FluentDocker/
COPY src/Persistence/*.csproj ./src/Persistence/
RUN dotnet restore

# copy everything else and build app
COPY src/. ./src/
WORKDIR /source/src/Api
RUN dotnet publish -c Release -o /app --no-restore --self-contained --runtime linux-x64

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./
EXPOSE 8080
ENTRYPOINT ["./Api"]
# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:3.1  as build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.sln .
COPY . .
WORKDIR /app/WebApi
RUN dotnet restore

# Copy everything else and build
RUN dotnet publish -c Realease -o out 

# Build runtime image
FROM mcr.microsoft.com/dotnet/sdk:3.1
WORKDIR /app
EXPOSE 80

COPY --from=build-env /app/WebApi/out ./
ENTRYPOINT ["dotnet","WebApi.dll" ]


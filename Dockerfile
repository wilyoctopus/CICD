## Setup build image
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app
ARG VER=1.0.0.0
ENV RELEASE_VERSION=${VER}

# Copy sln and csprojs, restore them
COPY CICD.sln ./
COPY CICD.UnitTests/*.csproj ./CICD.UnitTests/
COPY CICD.WebAPI/*.csproj ./CICD.WebAPI/

RUN dotnet restore

# Copy everything else, build, test and publish
COPY . .
RUN dotnet build
RUN dotnet test --logger xunit --collect "XPlat Code Coverage"
RUN dotnet publish CICD.WebAPI -c Release -o publish

## Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/publish .
EXPOSE 5000
ENTRYPOINT ["dotnet", "CICD.WebAPI.dll", "--server.urls", "http://*:5000"]


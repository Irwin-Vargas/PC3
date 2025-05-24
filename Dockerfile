# Stage 1: build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY *.sln .
COPY NewsPortalMVC/*.csproj ./NewsPortalMVC/
RUN dotnet restore

COPY . .
WORKDIR /app/NewsPortalMVC
RUN dotnet publish -c Release -o out

# Stage 2: run
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/NewsPortalMVC/out .

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

ENTRYPOINT ["dotnet", "NewsPortalMVC.dll"]

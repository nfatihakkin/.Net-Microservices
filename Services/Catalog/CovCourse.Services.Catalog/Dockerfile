FROM mcr.microsoft.com/dotnet/sdk:7.0-bookworm-slim as build
WORKDIR /app
EXPOSE 80
COPY Shared/CovCourse.Shared/*.csproj Shared/CovCourse.Shared/
COPY Services/Catalog/CovCourse.Services.Catalog/*.csproj Services/Catalog/CovCourse.Services.Catalog/
RUN dotnet restore Services/Catalog/CovCourse.Services.Catalog/*.csproj
COPY . .
RUN dotnet publish Services/Catalog/CovCourse.Services.Catalog/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:7.0-bookworm-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","CovCourse.Services.Catalog.dll" ]
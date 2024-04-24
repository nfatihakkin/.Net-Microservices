FROM mcr.microsoft.com/dotnet/sdk:7.0-bookworm-slim as build
WORKDIR /app
EXPOSE 80

COPY CovCourse.Gateway/*.csproj CovCourse.Gateway/
RUN dotnet restore CovCourse.Gateway/*.csproj
COPY . .
RUN dotnet publish CovCourse.Gateway/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:7.0-bookworm-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","CovCourse.Gateway.dll" ]
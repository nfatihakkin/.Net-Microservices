FROM mcr.microsoft.com/dotnet/sdk:7.0-bookworm-slim as build
WORKDIR /app
EXPOSE 80
COPY Shared/CovCourse.Shared/*.csproj Shared/CovCourse.Shared/
COPY Services/PhotoStock/CovCourse.Services.PhotoStock/*.csproj Services/PhotoStock/CovCourse.Services.PhotoStock/
RUN dotnet restore Services/PhotoStock/CovCourse.Services.PhotoStock/*.csproj
COPY . .
RUN dotnet publish Services/PhotoStock/CovCourse.Services.PhotoStock/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:7.0-bookworm-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","CovCourse.Services.PhotoStock.dll" ]
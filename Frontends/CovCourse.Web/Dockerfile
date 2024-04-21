FROM mcr.microsoft.com/dotnet/sdk:7.0-bookworm-slim as build
WORKDIR /app
EXPOSE 80
COPY Shared/CovCourse.Shared/*.csproj Shared/CovCourse.Shared/
COPY Frontends/CovCourse.Web/*.csproj Frontends/CovCourse.Web/ 
RUN dotnet restore  Frontends/CovCourse.Web/*.csproj
COPY . .
RUN dotnet publish Frontends/CovCourse.Web/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:7.0-bookworm-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","CovCourse.Web.dll" ]





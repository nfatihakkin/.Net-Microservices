FROM mcr.microsoft.com/dotnet/sdk:7.0-bookworm-slim as build
WORKDIR /app
EXPOSE 80
COPY Shared/CovCourse.Shared/*.csproj Shared/CovCourse.Shared/
COPY Services/Basket/CovCourse.Services.Basket/*.csproj Services/Basket/CovCourse.Services.Basket/
RUN dotnet restore Services/Basket/CovCourse.Services.Basket/*.csproj
COPY . .
RUN dotnet publish Services/Basket/CovCourse.Services.Basket/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:7.0-bookworm-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","CovCourse.Services.Basket.dll" ]
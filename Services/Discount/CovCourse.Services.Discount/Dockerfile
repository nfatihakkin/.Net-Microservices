FROM mcr.microsoft.com/dotnet/sdk:7.0-bookworm-slim as build
WORKDIR /app
EXPOSE 80
COPY Shared/CovCourse.Shared/*.csproj Shared/CovCourse.Shared/
COPY Services/Discount/CovCourse.Services.Discount/*.csproj Services/Discount/CovCourse.Services.Discount/
RUN dotnet restore Services/Discount/CovCourse.Services.Discount/*.csproj
COPY . .
RUN dotnet publish Services/Discount/CovCourse.Services.Discount/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:7.0-bookworm-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","CovCourse.Services.Discount.dll" ]
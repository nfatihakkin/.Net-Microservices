FROM mcr.microsoft.com/dotnet/sdk:7.0-bookworm-slim as build
WORKDIR /app
EXPOSE 80
COPY Shared/CovCourse.Shared/*.csproj Shared/CovCourse.Shared/

COPY Services/Order/CovCourse.Services.Order.Application/*.csproj Services/Order/CovCourse.Services.Order.Application/
COPY Services/Order/CovCourse.Services.Order.Domain/*.csproj Services/Order/CovCourse.Services.Order.Domain/
COPY Services/Order/CovCourse.Services.Order.Domain.Core/*.csproj Services/Order/CovCourse.Services.Order.Domain.Core/
COPY Services/Order/CovCourse.Services.Order.Infrastucture/*.csproj Services/Order/CovCourse.Services.Order.Infrastucture/

COPY Services/Order/CovCourse.Services.Order.API/*.csproj Services/Order/CovCourse.Services.Order.API/
RUN dotnet restore Services/Order/CovCourse.Services.Order.API/*.csproj
COPY . .
RUN dotnet publish Services/Order/CovCourse.Services.Order.API/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:7.0-bookworm-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","CovCourse.Services.Order.API.dll" ]
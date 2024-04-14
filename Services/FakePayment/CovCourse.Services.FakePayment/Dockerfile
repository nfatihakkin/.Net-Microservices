FROM mcr.microsoft.com/dotnet/sdk:7.0-bookworm-slim as build
WORKDIR /app
EXPOSE 80
COPY Shared/CovCourse.Shared/*.csproj Shared/CovCourse.Shared/
COPY Services/FakePayment/CovCourse.Services.FakePayment/*.csproj Services/FakePayment/CovCourse.Services.FakePayment/
RUN dotnet restore Services/FakePayment/CovCourse.Services.FakePayment/*.csproj
COPY . .
RUN dotnet publish Services/FakePayment/CovCourse.Services.FakePayment/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:7.0-bookworm-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","CovCourse.Services.FakePayment.dll" ]
FROM mcr.microsoft.com/dotnet/sdk:3.1 as build
WORKDIR /app
EXPOSE 80
COPY Shared/CovCourse.Shared/*.csproj Shared/CovCourse.Shared/
COPY IdentityServer/CovCourse.IdentityServer/*.csproj IdentityServer/CovCourse.IdentityServer/ 
RUN dotnet restore  IdentityServer/CovCourse.IdentityServer/*.csproj
COPY . .
RUN dotnet publish IdentityServer/CovCourse.IdentityServer/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:3.1 as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","CovCourse.IdentityServer.dll" ]





FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src
COPY ["src/Restro.Web.Host/Restro.Web.Host.csproj", "src/Restro.Web.Host/"]
COPY ["src/Restro.Web.Core/Restro.Web.Core.csproj", "src/Restro.Web.Core/"]
COPY ["src/Restro.Application/Restro.Application.csproj", "src/Restro.Application/"]
COPY ["src/Restro.Core/Restro.Core.csproj", "src/Restro.Core/"]
COPY ["src/Restro.EntityFrameworkCore/Restro.EntityFrameworkCore.csproj", "src/Restro.EntityFrameworkCore/"]
WORKDIR "/src/src/Restro.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/Restro.Web.Host", "src/Restro.Web.Host"]
COPY ["src/Restro.Web.Core", "src/Restro.Web.Core"]
COPY ["src/Restro.Application", "src/Restro.Application"]
COPY ["src/Restro.Core", "src/Restro.Core"]
COPY ["src/Restro.EntityFrameworkCore", "src/Restro.EntityFrameworkCore"]
WORKDIR "/src/src/Restro.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "Restro.Web.Host.dll"]

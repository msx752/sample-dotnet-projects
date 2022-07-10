#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["netCoreAPI/netCoreAPI.csproj", "netCoreAPI/"]
COPY ["netCoreAPI.Data/netCoreAPI.Data.csproj", "netCoreAPI.Data/"]
COPY ["netCoreAPI.Static/netCoreAPI.Static.csproj", "netCoreAPI.Static/"]
COPY ["netCoreAPI.Core/netCoreAPI.Core.csproj", "netCoreAPI.Core/"]
COPY ["netCoreAPI.Controllers/netCoreAPI.Controllers.csproj", "netCoreAPI.Controllers/"]
COPY ["netCoreAPI.DbSeed/netCoreAPI.DbSeed.csproj", "netCoreAPI.DbSeed/"]
RUN dotnet restore "netCoreAPI/netCoreAPI.csproj"
COPY . .
WORKDIR "/src/netCoreAPI"
RUN dotnet build "netCoreAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "netCoreAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#RUN echo "Europe/London" > /etc/timezone
ENTRYPOINT ["dotnet", "netCoreAPI.dll"]
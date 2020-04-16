#docker run -d -it --restart unless-stopped --net=host -p 5501:5501 -p 5500:5500 dockername/repository:tag
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-bionic AS base
WORKDIR /app
EXPOSE 5501
EXPOSE 5500

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-bionic AS build
WORKDIR /src
COPY ["netCoreAPI/netCoreAPI.csproj", "netCoreAPI/"]
COPY ["netCoreAPI.Model/netCoreAPI.Model.csproj", "netCoreAPI.Model/"]
COPY ["netCoreAPI.Data/netCoreAPI.Data.csproj", "netCoreAPI.Data/"]
COPY ["netCoreAPI.Static/netCoreAPI.Static.csproj", "netCoreAPI.Static/"]
COPY ["netCoreAPI.Core/netCoreAPI.Core.csproj", "netCoreAPI.Core/"]
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
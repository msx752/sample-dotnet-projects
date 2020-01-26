#docker run -d -it --restart unless-stopped --net=host -p 80:80 -p 444:444 mustafasalih/test:tag1
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-bionic AS base
WORKDIR /app
EXPOSE 5001
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-bionic AS build
WORKDIR /src
COPY ["netCoreAPITest/netCoreAPITest.csproj", "netCoreAPITest/"]
COPY ["netCoreAPITest.Data/netCoreAPITest.Data.csproj", "netCoreAPITest.Data/"]
RUN dotnet restore "netCoreAPITest/netCoreAPITest.csproj"
COPY . .
WORKDIR "/src/netCoreAPITest"
RUN dotnet build "netCoreAPITest.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "netCoreAPITest.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
#RUN echo "Europe/London" > /etc/timezone
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "netCoreAPITest.dll"]
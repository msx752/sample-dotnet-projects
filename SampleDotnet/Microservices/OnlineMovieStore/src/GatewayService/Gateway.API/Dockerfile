#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Microservices/OnlineMovieStore/src/GatewayService/Gateway.API/Gateway.API.csproj", "Microservices/OnlineMovieStore/src/GatewayService/Gateway.API/"]
COPY ["Microservices/OnlineMovieStore/src/OnlineMovieStore.Core/OnlineMovieStore.Core.csproj", "Microservices/OnlineMovieStore/src/OnlineMovieStore.Core/"]
COPY ["NugetPackages/SampleProject.Authentication/SampleProject.Authentication.csproj", "NugetPackages/SampleProject.Authentication/"]
COPY ["NugetPackages/SampleProject.Database/SampleProject.Database.csproj", "NugetPackages/SampleProject.Database/"]
COPY ["NugetPackages/SampleProject.Result/SampleProject.Result.csproj", "NugetPackages/SampleProject.Result/"]
RUN dotnet restore "Microservices/OnlineMovieStore/src/GatewayService/Gateway.API/Gateway.API.csproj"
COPY . .
WORKDIR "/src/Microservices/OnlineMovieStore/src/GatewayService/Gateway.API"
RUN dotnet build "Gateway.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gateway.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gateway.API.dll"]
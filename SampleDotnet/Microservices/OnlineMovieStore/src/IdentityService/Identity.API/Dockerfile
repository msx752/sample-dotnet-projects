#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Microservices/OnlineMovieStore/src/IdentityService/Identity.API/Identity.API.csproj", "Microservices/OnlineMovieStore/src/IdentityService/Identity.API/"]
COPY ["Microservices/OnlineMovieStore/src/IdentityService/Identity.Database/Identity.Database.csproj", "Microservices/OnlineMovieStore/src/IdentityService/Identity.Database/"]
COPY ["Microservices/OnlineMovieStore/src/OnlineMovieStore.Core/OnlineMovieStore.Core.csproj", "Microservices/OnlineMovieStore/src/OnlineMovieStore.Core/"]
COPY ["NugetPackages/SampleProject.Authentication/SampleProject.Authentication.csproj", "NugetPackages/SampleProject.Authentication/"]
COPY ["NugetPackages/SampleDotnet.RepositoryFactory/SampleDotnet.RepositoryFactory.csproj", "NugetPackages/SampleDotnet.RepositoryFactory/"]
RUN dotnet restore "Microservices/OnlineMovieStore/src/IdentityService/Identity.API/Identity.API.csproj"
COPY . .
WORKDIR "/src/Microservices/OnlineMovieStore/src/IdentityService/Identity.API"
RUN dotnet build "Identity.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Identity.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Identity.API.dll"]
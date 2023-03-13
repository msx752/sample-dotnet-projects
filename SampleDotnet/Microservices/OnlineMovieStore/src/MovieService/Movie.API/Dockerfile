#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Microservices/OnlineMovieStore/src/MovieService/Movie.API/Movie.API.csproj", "Microservices/OnlineMovieStore/src/MovieService/Movie.API/"]
COPY ["NugetPackages/SampleProject.Contract/SampleProject.Contract.csproj", "NugetPackages/SampleProject.Contract/"]
COPY ["Microservices/OnlineMovieStore/src/MovieService/Movie.Database/Movie.Database.csproj", "Microservices/OnlineMovieStore/src/MovieService/Movie.Database/"]
COPY ["Microservices/OnlineMovieStore/src/OnlineMovieStore.Core/OnlineMovieStore.Core.csproj", "Microservices/OnlineMovieStore/src/OnlineMovieStore.Core/"]
COPY ["NugetPackages/SampleProject.Authentication/SampleProject.Authentication.csproj", "NugetPackages/SampleProject.Authentication/"]
RUN dotnet restore "Microservices/OnlineMovieStore/src/MovieService/Movie.API/Movie.API.csproj"
COPY . .
WORKDIR "/src/Microservices/OnlineMovieStore/src/MovieService/Movie.API"
RUN dotnet build "Movie.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Movie.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Movie.API.dll"]
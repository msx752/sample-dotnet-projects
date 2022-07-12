#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/MovieService/Samp.Movie.API/Samp.Movie.API.csproj", "src/MovieService/Samp.Movie.API/"]
COPY ["src/MovieService/Samp.Movie.Database/Samp.Movie.Database.csproj", "src/MovieService/Samp.Movie.Database/"]
COPY ["src/Samp.Core/Samp.Core.csproj", "src/Samp.Core/"]
RUN dotnet restore "src/MovieService/Samp.Movie.API/Samp.Movie.API.csproj"
COPY . .
WORKDIR "/src/src/MovieService/Samp.Movie.API"
RUN dotnet build "Samp.Movie.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Samp.Movie.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#RUN echo "Europe/London" > /etc/timezone
ENTRYPOINT ["dotnet", "Samp.Movie.API.dll"]
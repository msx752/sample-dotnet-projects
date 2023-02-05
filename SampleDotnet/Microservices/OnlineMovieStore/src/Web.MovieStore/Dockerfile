#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
# Install NodeJs
RUN apt-get update && \
apt-get install -y wget && \
apt-get install -y gnupg2 && \
wget -qO- https://deb.nodesource.com/setup_16.x | bash - && \
apt-get install -y --allow-unauthenticated build-essential nodejs
# End Install
WORKDIR /src
COPY ["Microservices/OnlineMovieStore/src/Web.MovieStore/Web.MovieStore.csproj", "Microservices/OnlineMovieStore/src/Web.MovieStore/"]
RUN dotnet restore "Microservices/OnlineMovieStore/src/Web.MovieStore/Web.MovieStore.csproj"
COPY . .
WORKDIR "/src/Microservices/OnlineMovieStore/src/Web.MovieStore"
RUN dotnet build "Web.MovieStore.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Web.MovieStore.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.MovieStore.dll"]
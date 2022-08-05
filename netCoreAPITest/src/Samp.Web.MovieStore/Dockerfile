#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mustafasalih/dotnetcore-runtime:7.0.0-preview.5-bullseye-slim-amd64 AS base

WORKDIR /app
EXPOSE 80

FROM mustafasalih/dotnetcore-sdk:7.0.0-preview.5-bullseye-slim-amd64 AS build
# Install NodeJs
RUN apt-get update && \
apt-get install -y wget && \
apt-get install -y gnupg2 && \
wget -qO- https://deb.nodesource.com/setup_16.x | bash - && \
apt-get install -y build-essential nodejs
# End Install
WORKDIR /src
COPY ["src/Samp.Web.MovieStore/Samp.Web.MovieStore.csproj", "src/Samp.Web.MovieStore/"]
RUN dotnet restore --no-cache "src/Samp.Web.MovieStore/Samp.Web.MovieStore.csproj"
COPY . .
WORKDIR "/src/src/Samp.Web.MovieStore"
RUN dotnet build "Samp.Web.MovieStore.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Samp.Web.MovieStore.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Samp.Web.MovieStore.dll"]
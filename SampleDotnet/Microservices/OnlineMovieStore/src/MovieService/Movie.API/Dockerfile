#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mustafasalih/dotnetcore-runtime:7.0.0-preview.5-bullseye-slim-amd64 AS base
WORKDIR /app
EXPOSE 80

FROM mustafasalih/dotnetcore-sdk:7.0.0-preview.5-bullseye-slim-amd64 AS build
WORKDIR /src
COPY ["src/MovieService/Samp.Movie.API/Samp.Movie.API.csproj", "src/MovieService/Samp.Movie.API/"]
COPY ["src/Samp.Contract/Samp.Contract.csproj", "src/Samp.Contract/"]
COPY ["src/MovieService/Samp.Movie.Database/Samp.Movie.Database.csproj", "src/MovieService/Samp.Movie.Database/"]
COPY ["src/Samp.Core/Samp.Core.csproj", "src/Samp.Core/"]
COPY ["src/Samp.Authentication/Samp.Authentication.csproj", "src/Samp.Authentication/"]
COPY ["src/Samp.Result/Samp.Result.csproj", "src/Samp.Result/"]
COPY ["src/Samp.Database/Samp.Database.csproj", "src/Samp.Database/"]
RUN dotnet restore "src/MovieService/Samp.Movie.API/Samp.Movie.API.csproj"
COPY . .
WORKDIR "/src/src/MovieService/Samp.Movie.API"
RUN dotnet build "Samp.Movie.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Samp.Movie.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Samp.Movie.API.dll"]
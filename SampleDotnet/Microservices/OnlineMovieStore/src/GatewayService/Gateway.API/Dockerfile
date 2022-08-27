#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mustafasalih/dotnetcore-runtime:7.0.0-preview.5-bullseye-slim-amd64 AS base
WORKDIR /app
EXPOSE 80

FROM mustafasalih/dotnetcore-sdk:7.0.0-preview.5-bullseye-slim-amd64 AS build
WORKDIR /src
COPY ["src/GatewayService/Gateway.API/Gateway.API.csproj", "src/GatewayService/Gateway.API/"]
COPY ["src/OnlineMovieStore.Core/OnlineMovieStore.Core.csproj", "src/OnlineMovieStore.Core/"]
COPY ["src/Samp.Database/SampleProject.Database.csproj", "src/Samp.Database/"]
COPY ["src/Samp.Authentication/SampleProject.Authentication.csproj", "src/Samp.Authentication/"]
COPY ["src/Samp.Result/SampleProject.Result.csproj", "src/Samp.Result/"]
RUN dotnet restore "src/GatewayService/Gateway.API/Gateway.API.csproj"
COPY . .
WORKDIR "/src/src/GatewayService/Gateway.API"
RUN dotnet build "Gateway.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gateway.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gateway.API.dll"]
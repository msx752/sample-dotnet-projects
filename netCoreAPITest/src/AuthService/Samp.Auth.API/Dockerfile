#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/AuthService/Samp.Auth.API/Samp.Identity.API.csproj", "src/AuthService/Samp.Auth.API/"]
COPY ["src/AuthService/Samp.Auth.Database/Samp.Identity.Database.csproj", "src/AuthService/Samp.Auth.Database/"]
COPY ["src/Samp.Core/Samp.Core.csproj", "src/Samp.Core/"]
RUN dotnet restore "src/AuthService/Samp.Auth.API/Samp.Identity.API.csproj"
COPY . .
WORKDIR "/src/src/AuthService/Samp.Auth.API"
RUN dotnet build "Samp.Identity.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Samp.Identity.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Samp.Identity.API.dll"]
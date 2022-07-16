#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mustafasalih/dotnetcore-runtime:7.0.0-preview.5-bullseye-slim-amd64 AS base
WORKDIR /app
EXPOSE 80

FROM mustafasalih/dotnetcore-sdk:7.0.0-preview.5-bullseye-slim-amd64 AS build
WORKDIR /src
COPY ["src/IdentityService/Samp.Identity.API/Samp.Identity.API.csproj", "src/IdentityService/Samp.Identity.API/"]
COPY ["src/IdentityService/Samp.Identity.Database/Samp.Identity.Database.csproj", "src/IdentityService/Samp.Identity.Database/"]
COPY ["src/Samp.Core/Samp.Core.csproj", "src/Samp.Core/"]
RUN dotnet restore "src/IdentityService/Samp.Identity.API/Samp.Identity.API.csproj"
COPY . .
WORKDIR "/src/src/IdentityService/Samp.Identity.API"
RUN dotnet build "Samp.Identity.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Samp.Identity.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Samp.Identity.API.dll"]
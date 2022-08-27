#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mustafasalih/dotnetcore-runtime:7.0.0-preview.5-bullseye-slim-amd64 AS base
WORKDIR /app
EXPOSE 80

FROM mustafasalih/dotnetcore-sdk:7.0.0-preview.5-bullseye-slim-amd64 AS build
WORKDIR /src
COPY ["src/IdentityService/Identity.API/Identity.API.csproj", "src/IdentityService/Identity.API/"]
COPY ["src/IdentityService/Identity.Database/Identity.Database.csproj", "src/IdentityService/Identity.Database/"]
COPY ["src/OnlineMovieStore.Core/OnlineMovieStore.Core.csproj", "src/OnlineMovieStore.Core/"]
COPY ["src/Samp.Database/SampleProject.Database.csproj", "src/Samp.Database/"]
COPY ["src/Samp.Authentication/SampleProject.Authentication.csproj", "src/Samp.Authentication/"]
COPY ["src/Samp.Result/SampleProject.Result.csproj", "src/Samp.Result/"]
RUN dotnet restore "src/IdentityService/Identity.API/Identity.API.csproj"
COPY . .
WORKDIR "/src/src/IdentityService/Identity.API"
RUN dotnet build "Identity.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Identity.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Identity.API.dll"]
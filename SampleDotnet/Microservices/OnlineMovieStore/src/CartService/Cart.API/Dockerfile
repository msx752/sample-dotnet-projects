#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Microservices/OnlineMovieStore/src/CartService/Cart.API/Cart.API.csproj", "Microservices/OnlineMovieStore/src/CartService/Cart.API/"]
COPY ["NugetPackages/SampleProject.Contract/SampleProject.Contract.csproj", "NugetPackages/SampleProject.Contract/"]
COPY ["Microservices/OnlineMovieStore/src/CartService/Cart.Database/Cart.Database.csproj", "Microservices/OnlineMovieStore/src/CartService/Cart.Database/"]
COPY ["Microservices/OnlineMovieStore/src/OnlineMovieStore.Core/OnlineMovieStore.Core.csproj", "Microservices/OnlineMovieStore/src/OnlineMovieStore.Core/"]
COPY ["NugetPackages/SampleProject.Authentication/SampleProject.Authentication.csproj", "NugetPackages/SampleProject.Authentication/"]
RUN dotnet restore "Microservices/OnlineMovieStore/src/CartService/Cart.API/Cart.API.csproj"
COPY . .
WORKDIR "/src/Microservices/OnlineMovieStore/src/CartService/Cart.API"
RUN dotnet build "Cart.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cart.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cart.API.dll"]
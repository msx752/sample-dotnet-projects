#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/CartService/Samp.Cart.API/Samp.Cart.API.csproj", "src/CartService/Samp.Cart.API/"]
COPY ["src/CartService/Samp.Cart.Database/Samp.Cart.Database.csproj", "src/CartService/Samp.Cart.Database/"]
COPY ["src/Samp.Core/Samp.Core.csproj", "src/Samp.Core/"]
RUN dotnet restore "src/CartService/Samp.Cart.API/Samp.Cart.API.csproj"
COPY . .
WORKDIR "/src/src/CartService/Samp.Cart.API"
RUN dotnet build "Samp.Cart.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Samp.Cart.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Samp.Cart.API.dll"]
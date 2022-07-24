#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mustafasalih/dotnetcore-runtime:7.0.0-preview.5-bullseye-slim-amd64 AS base
WORKDIR /app
EXPOSE 80

FROM mustafasalih/dotnetcore-sdk:7.0.0-preview.5-bullseye-slim-amd64 AS build
WORKDIR /src
COPY ["src/CartService/Samp.Cart.API/Samp.Cart.API.csproj", "src/CartService/Samp.Cart.API/"]
COPY ["src/CartService/Samp.Cart.Database/Samp.Cart.Database.csproj", "src/CartService/Samp.Cart.Database/"]
COPY ["src/Samp.Core/Samp.Core.csproj", "src/Samp.Core/"]
COPY ["src/Samp.Contract/Samp.Contract.csproj", "src/Samp.Contract/"]
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
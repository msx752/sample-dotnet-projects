#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mustafasalih/dotnetcore-runtime:7.0.0-preview.5-bullseye-slim-amd64 AS base
WORKDIR /app
EXPOSE 80

FROM mustafasalih/dotnetcore-sdk:7.0.0-preview.5-bullseye-slim-amd64 AS build
WORKDIR /src
COPY ["src/CartService/Cart.API/Cart.API.csproj", "src/CartService/Cart.API/"]
COPY ["src/CartService/Cart.Database/Cart.Database.csproj", "src/CartService/Cart.Database/"]
COPY ["src/OnlineMovieStore.Core/OnlineMovieStore.Core.csproj", "src/OnlineMovieStore.Core/"]
COPY ["src/Samp.Database/SampleProject.Database.csproj", "src/Samp.Database/"]
COPY ["src/Samp.Authentication/SampleProject.Authentication.csproj", "src/Samp.Authentication/"]
COPY ["src/Samp.Result/SampleProject.Result.csproj", "src/Samp.Result/"]
COPY ["src/Samp.Contract/SampleProject.Contract.csproj", "src/Samp.Contract/"]
RUN dotnet restore "src/CartService/Cart.API/Cart.API.csproj"
COPY . .
WORKDIR "/src/src/CartService/Cart.API"
RUN dotnet build "Cart.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cart.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cart.API.dll"]
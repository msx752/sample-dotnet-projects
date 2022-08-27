#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mustafasalih/dotnetcore-runtime:7.0.0-preview.5-bullseye-slim-amd64 AS base
WORKDIR /app
EXPOSE 80

FROM mustafasalih/dotnetcore-sdk:7.0.0-preview.5-bullseye-slim-amd64 AS build
WORKDIR /src
COPY ["Microservices/OnlineMovieStore/src/CartService/Cart.API/Cart.API.csproj", "Microservices/OnlineMovieStore/src/CartService/Cart.API/"]
COPY ["Microservices/OnlineMovieStore/src/CartService/Cart.Database/Cart.Database.csproj", "Microservices/OnlineMovieStore/src/CartService/Cart.Database/"]
COPY ["Microservices/OnlineMovieStore/src/OnlineMovieStore.Core/OnlineMovieStore.Core.csproj", "Microservices/OnlineMovieStore/src/OnlineMovieStore.Core/"]
COPY ["NugetPackages/SampleProject.Authentication/SampleProject.Authentication.csproj", "NugetPackages/SampleProject.Authentication/"]
COPY ["NugetPackages/SampleProject.Result/SampleProject.Result.csproj", "NugetPackages/SampleProject.Result/"]
COPY ["NugetPackages/SampleProject.Database/SampleProject.Database.csproj", "NugetPackages/SampleProject.Database/"]
COPY ["NugetPackages/SampleProject.Contract/SampleProject.Contract.csproj", "NugetPackages/SampleProject.Contract/"]
RUN dotnet restore "Microservices/OnlineMovieStore/src/CartService/Cart.API/Cart.API.csproj"
COPY . .
WORKDIR "/src/Microservices/OnlineMovieStore/src/CartService/Cart.API"
RUN dotnet build "Cart.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cart.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cart.API.dll"]
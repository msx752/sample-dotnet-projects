#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mustafasalih/dotnetcore-runtime:7.0.0-preview.5-bullseye-slim-amd64 AS base
WORKDIR /app
EXPOSE 80

FROM mustafasalih/dotnetcore-sdk:7.0.0-preview.5-bullseye-slim-amd64 AS build
WORKDIR /src
COPY ["Microservices/OnlineMovieStore/src/PaymentService/Payment.API/Payment.API.csproj", "Microservices/OnlineMovieStore/src/PaymentService/Payment.API/"]
COPY ["Microservices/OnlineMovieStore/src/PaymentService/Payment.Database/Payment.Database.csproj", "Microservices/OnlineMovieStore/src/PaymentService/Payment.Database/"]
COPY ["Microservices/OnlineMovieStore/src/OnlineMovieStore.Core/OnlineMovieStore.Core.csproj", "Microservices/OnlineMovieStore/src/OnlineMovieStore.Core/"]
COPY ["NugetPackages/SampleProject.Authentication/SampleProject.Authentication.csproj", "NugetPackages/SampleProject.Authentication/"]
COPY ["NugetPackages/SampleProject.Result/SampleProject.Result.csproj", "NugetPackages/SampleProject.Result/"]
COPY ["NugetPackages/SampleProject.Database/SampleProject.Database.csproj", "NugetPackages/SampleProject.Database/"]
COPY ["NugetPackages/SampleProject.Contract/SampleProject.Contract.csproj", "NugetPackages/SampleProject.Contract/"]
RUN dotnet restore "Microservices/OnlineMovieStore/src/PaymentService/Payment.API/Payment.API.csproj"
COPY . .
WORKDIR "/src/Microservices/OnlineMovieStore/src/PaymentService/Payment.API"
RUN dotnet build "Payment.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Payment.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Payment.API.dll"]
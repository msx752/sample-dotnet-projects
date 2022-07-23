#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mustafasalih/dotnetcore-runtime:7.0.0-preview.5-bullseye-slim-amd64 AS base
WORKDIR /app
EXPOSE 80

FROM mustafasalih/dotnetcore-sdk:7.0.0-preview.5-bullseye-slim-amd64 AS build
WORKDIR /src
COPY ["src/PaymentService/Samp.Payment.API/Samp.Payment.API.csproj", "src/PaymentService/Samp.Payment.API/"]
COPY ["src/Samp.Contract/Samp.Contract.csproj", "src/Samp.Contract/"]
COPY ["src/PaymentService/Samp.Payment.Database/Samp.Payment.Database.csproj", "src/PaymentService/Samp.Payment.Database/"]
COPY ["src/Samp.Core/Samp.Core.csproj", "src/Samp.Core/"]
RUN dotnet restore "src/PaymentService/Samp.Payment.API/Samp.Payment.API.csproj"
COPY . .
WORKDIR "/src/src/PaymentService/Samp.Payment.API"
RUN dotnet build "Samp.Payment.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Samp.Payment.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Samp.Payment.API.dll"]
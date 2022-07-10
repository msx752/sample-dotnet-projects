#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/PersonalService/Samp.Personal.API/Samp.Personal.API.csproj", "src/PersonalService/Samp.Personal.API/"]
COPY ["src/PersonalService/Samp.Personal.Database/Samp.Personal.Database.csproj", "src/PersonalService/Samp.Personal.Database/"]
COPY ["src/Samp.Core/Samp.Core.csproj", "src/Samp.Core/"]
RUN dotnet restore "src/PersonalService/Samp.Personal.API/Samp.Personal.API.csproj"
COPY . .
WORKDIR "/src/src/PersonalService/Samp.Personal.API"
RUN dotnet build "Samp.Personal.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Samp.Personal.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#RUN echo "Europe/London" > /etc/timezone
ENTRYPOINT ["dotnet", "Samp.Personal.API.dll"]
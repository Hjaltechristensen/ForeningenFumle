# Brug en officiel .NET runtime som base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Brug en officiel .NET SDK som build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Kopier og byg applikationen
COPY ["ForeningenFumle/Client/ForeningenFumle.Client.csproj", "Client/"]
RUN dotnet restore "C:/Users/hjalt/source/repos/ForeningenFumle/ForeningenFumle.sln"

COPY . .
WORKDIR "/src/Client"
RUN dotnet build "/ForeningenFumle/ForeningenFumle.sln" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ForeningenFumle.Client.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ForeningenFumle.Client.dll"]
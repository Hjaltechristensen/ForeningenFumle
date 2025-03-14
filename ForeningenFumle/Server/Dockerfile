# Base image for the server
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Build image for both server and client
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy server project
COPY ["ForeningenFumle/Server/ForeningenFumle.Server.csproj", "ForeningenFumle/Server/"]
RUN dotnet restore "ForeningenFumle/Server/ForeningenFumle.Server.csproj"

# Copy client project (Blazor WebAssembly)
COPY ["ForeningenFumle/Client/ForeningenFumle.Client.csproj", "ForeningenFumle/Client/"]
RUN dotnet restore "ForeningenFumle/Client/ForeningenFumle.Client.csproj"

# Copy all source files
COPY . .

# Build both server and client
WORKDIR "/src/ForeningenFumle/Client"
RUN dotnet build "ForeningenFumle.Client.csproj" -c Release -o /app/build

WORKDIR "/src/ForeningenFumle/Server"
RUN dotnet build "ForeningenFumle.Server.csproj" -c Release -o /app/build

# Publish both server and client
FROM build AS publish
RUN dotnet publish "ForeningenFumle/Client/ForeningenFumle.Client.csproj" -c Release -o /app/publish/client
RUN dotnet publish "ForeningenFumle/Server/ForeningenFumle.Server.csproj" -c Release -o /app/publish/server

# Final image for the server
FROM base AS final
WORKDIR /app

# Copy published server and client files
COPY --from=publish /app/publish/server ./
COPY --from=publish /app/publish/client /app/wwwroot

# Set entrypoint for the server
ENTRYPOINT ["dotnet", "ForeningenFumle.Server.dll"]

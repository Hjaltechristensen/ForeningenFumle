FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ForeningenFumle/Server/ForeningenFumle.Server.csproj", "ForeningenFumle/Server/"]
RUN dotnet restore "ForeningenFumle/Server/ForeningenFumle.Server.csproj"
COPY . .
WORKDIR "/src/ForeningenFumle/Server"
RUN dotnet build "ForeningenFumle.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ForeningenFumle.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ForeningenFumle.Server.dll"] 

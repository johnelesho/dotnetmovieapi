FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DotNetMoviesApi/DotNetMoviesApi.csproj", "DotNetMoviesApi/"]
RUN dotnet restore "DotNetMoviesApi/DotNetMoviesApi.csproj"
COPY . .
WORKDIR "/src/DotNetMoviesApi"
RUN dotnet build "DotNetMoviesApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DotNetMoviesApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DotNetMoviesApi.dll"]

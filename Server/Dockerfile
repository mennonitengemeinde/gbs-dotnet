FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR "/src/Client"
RUN curl -sL https://deb.nodesource.com/setup_16.x | bash - && \
    apt-get install -y nodejs build-essential
RUN npm install && npm run build
WORKDIR /src
COPY ["Server/gbs.Server.csproj", "Server/"]
COPY ["Client/gbs.Client.csproj", "Client/"]
COPY ["Shared/gbs.Shared.csproj", "Shared/"]
RUN dotnet restore "Server/gbs.Server.csproj"
COPY . .
WORKDIR "/src/Server"
RUN dotnet build "gbs.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "gbs.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "gbs.Server.dll"]

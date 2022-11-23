#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
RUN curl -sL https://deb.nodesource.com/setup_16.x | bash - && \
    apt-get install -y nodejs build-essential
WORKDIR /src
COPY ["src/Domain/Gbs.Domain.csproj", "Domain/"]
COPY ["src/Shared/Gbs.Shared.csproj", "Shared/"]
COPY ["src/Application/Gbs.Application.csproj", "Application/"]
COPY ["src/Infrastructure.Persistence/Gbs.Infrastructure.Persistence.csproj", "Infrastructure.Persistence/"]
COPY ["src/Api/Gbs.Api.csproj", "Api/"]
COPY ["src/Wasm/Gbs.Wasm.csproj", "Wasm/"]
RUN dotnet restore "Api/Gbs.Api.csproj"
COPY ./src .
WORKDIR "/src/Wasm"
RUN npm install && npm run build
WORKDIR "/src/Api"
RUN dotnet build "Gbs.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gbs.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gbs.Api.dll"]
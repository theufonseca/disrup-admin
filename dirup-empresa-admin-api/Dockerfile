#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["dirup-empresa-admin-api/dirup-empresa-admin-api.csproj", "dirup-empresa-admin-api/"]
RUN dotnet restore "dirup-empresa-admin-api/dirup-empresa-admin-api.csproj"
COPY . .
WORKDIR "/src/dirup-empresa-admin-api"
RUN dotnet build "dirup-empresa-admin-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "dirup-empresa-admin-api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "dirup-empresa-admin-api.dll"]
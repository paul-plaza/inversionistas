FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Investors.Administrator.Api/Investors.Administrator.Api.csproj", "Investors.Administrator.Api/"]
RUN dotnet restore "Investors.Administrator.Api/Investors.Administrator.Api.csproj"
COPY . .
WORKDIR "/src/Investors.Administrator.Api"
RUN dotnet build "Investors.Administrator.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Investors.Administrator.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Investors.Administrator.Api.dll"]

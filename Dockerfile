FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PayrollApi/PayrollApi.csproj", "PayrollApi/"]
RUN dotnet restore "PayrollApi/PayrollApi.csproj"
COPY . .
WORKDIR "/src/PayrollApi"
RUN dotnet build "PayrollApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PayrollApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PayrollApi.dll"]

#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Rolls/Rolls.csproj", "Rolls/"]
COPY ["src/Common/Common.csproj", "Common/"]
RUN dotnet restore "src/Rolls/Rolls.csproj"
COPY . .
WORKDIR "/src/Rolls"
RUN dotnet build "Rolls.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Rolls.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 5000
ENTRYPOINT ["dotnet", "Rolls.dll"]

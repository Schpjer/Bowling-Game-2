
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Presenter/Presenter.csproj", "Presenter/"]
COPY ["src/Common/Common.csproj", "Common/"]
RUN dotnet restore "Presenter/Presenter.csproj"
COPY . .
WORKDIR "/src/Presenter"
RUN dotnet build "Presenter.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Presenter.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 5000
ENTRYPOINT ["dotnet", "Presenter.dll"]
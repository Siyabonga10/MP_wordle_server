FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["MP_WORDLE_SERVER.csproj", "./"]
RUN dotnet restore "MP_WORDLE_SERVER.csproj"
COPY . .
RUN dotnet build "MP_WORDLE_SERVER.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MP_WORDLE_SERVER.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MP_WORDLE_SERVER.dll"]
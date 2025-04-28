FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base
WORKDIR /app
EXPOSE 3008

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["MiniChatApp.Server/MiniChatApp.Server.csproj", "MiniChatApp.Server/"]
RUN dotnet restore "MiniChatApp.Server/MiniChatApp.Server.csproj"
COPY . .
WORKDIR "/src/MiniChatApp.Server"
RUN dotnet build "MiniChatApp.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MiniChatApp.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MiniChatApp.Server.dll"]

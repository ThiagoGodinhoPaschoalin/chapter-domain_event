#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WebAppWithEventDelegateV2/WebAppWithEventDelegateV2.csproj", "WebAppWithEventDelegateV2/"]
COPY ["SharedDomain/SharedDomain.csproj", "SharedDomain/"]
RUN dotnet restore "WebAppWithEventDelegateV2/WebAppWithEventDelegateV2.csproj"
COPY . .
WORKDIR "/src/WebAppWithEventDelegateV2"
RUN dotnet build "WebAppWithEventDelegateV2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebAppWithEventDelegateV2.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebAppWithEventDelegateV2.dll"]
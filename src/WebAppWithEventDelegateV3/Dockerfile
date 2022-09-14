#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WebAppWithEventDelegateV3/WebAppWithEventDelegateV3.csproj", "WebAppWithEventDelegateV3/"]
COPY ["SharedDomain/SharedDomain.csproj", "SharedDomain/"]
RUN dotnet restore "WebAppWithEventDelegateV3/WebAppWithEventDelegateV3.csproj"
COPY . .
WORKDIR "/src/WebAppWithEventDelegateV3"
RUN dotnet build "WebAppWithEventDelegateV3.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebAppWithEventDelegateV3.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebAppWithEventDelegateV3.dll"]
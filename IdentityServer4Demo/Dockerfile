FROM microsoft/aspnetcore:2.1-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.1-nanoserver-1709 AS build
WORKDIR /src
COPY WebSocketDemo.sln ./
COPY IdentityServer4Demo/IdentityServer4Demo.csproj IdentityServer4Demo/
RUN dotnet restore -nowarn:msb3202,nu1503
COPY . .
WORKDIR /src/IdentityServer4Demo
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "IdentityServer4Demo.dll"]

FROM microsoft/aspnetcore:2.0 AS base
#WORKDIR /app
EXPOSE 5000

FROM microsoft/aspnetcore-build:2.0 AS build
#WORKDIR /src
#RUN ls -la Backend/
#COPY *.sln ./
#COPY Backend.csproj Backend/
#RUN dotnet restore
COPY . .
RUN dotnet restore
#WORKDIR /src/Backend
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Backend.dll"]

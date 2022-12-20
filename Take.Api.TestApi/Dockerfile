FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine3.11 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine3.11 AS build
WORKDIR /src
COPY ["Take.Api.TestApi.Facades/Take.Api.TestApi.Facades.csproj", "Take.Api.TestApi.Facades/"]
COPY ["Take.Api.TestApi.Models/Take.Api.TestApi.Models.csproj", "Take.Api.TestApi.Models/"]
COPY ["Take.Api.TestApi.Services/Take.Api.TestApi.Services.csproj", "Take.Api.TestApi.Services/"]
COPY ["Take.Api.TestApi/Take.Api.TestApi.csproj", "Take.Api.TestApi/"]

RUN dotnet restore "Take.Api.TestApi/Take.Api.TestApi.csproj"
COPY . .
WORKDIR "/src/Take.Api.TestApi"
RUN dotnet build "Take.Api.TestApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Take.Api.TestApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Take.Api.TestApi.dll"]
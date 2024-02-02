#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
RUN apt-get update && apt-get install -y apt-utils libgdiplus libc6-dev
#RUN apt-get update && apt-get install -y libgdiplus
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["src/Account.Microservice.Web/Account.Microservice.Web.csproj", "src/Account.Microservice.Web/"]
COPY ["src/Account.Microservice.Infrastructure/Account.Microservice.Infrastructure.csproj", "src/Account.Microservice.Infrastructure/"]
COPY ["src/Account.Microservice.Core/Account.Microservice.Core.csproj", "src/Account.Microservice.Core/"]
COPY ["src/Account.Microservice.SharedKernel/Account.Microservice.SharedKernel.csproj", "src/Account.Microservice.SharedKernel/"]
RUN dotnet restore "src/Account.Microservice.Web/Account.Microservice.Web.csproj"
COPY . .
WORKDIR "/src/src/Account.Microservice.Web"
ENV ASPNETCORE_ENVIRONMENT Production
RUN dotnet build "Account.Microservice.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Account.Microservice.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
ENV ASPNETCORE_ENVIRONMENT Production
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Account.Microservice.Web.dll"]
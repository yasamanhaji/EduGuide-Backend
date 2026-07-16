# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Gateway/EduGuide.Gateway/EduGuide.Gateway.csproj", "src/Gateway/EduGuide.Gateway/"]
COPY ["src/Common/Base/Base.Api/Base.Api.csproj", "src/Common/Base/Base.Api/"]
COPY ["src/Common/Base.Utilities/Base.Utilities.csproj", "src/Common/Base.Utilities/"]
COPY ["src/Common/Base/Base.Application/Base.Application.csproj", "src/Common/Base/Base.Application/"]
COPY ["src/Common/Base/Base.Application.Contracts/Base.Application.Contracts.csproj", "src/Common/Base/Base.Application.Contracts/"]
COPY ["src/Common/Base/Base.Domain/Base.Domain.csproj", "src/Common/Base/Base.Domain/"]
COPY ["src/Common/Base/Base.Infrastructure/Base.Infrastructure.csproj", "src/Common/Base/Base.Infrastructure/"]
COPY ["src/Modules/EduGuide/EduGuide.Application/EduGuide.Application.csproj", "src/Modules/EduGuide/EduGuide.Application/"]
COPY ["src/Modules/EduGuide/EduGuide.Domain/EduGuide.Domain.csproj", "src/Modules/EduGuide/EduGuide.Domain/"]
COPY ["src/Modules/EduGuide/EduGuide.Infrastructure/EduGuide.Infrastructure.csproj", "src/Modules/EduGuide/EduGuide.Infrastructure/"]
RUN dotnet restore "./src/Gateway/EduGuide.Gateway/EduGuide.Gateway.csproj"
COPY . .
WORKDIR "/src/src/Gateway/EduGuide.Gateway"
RUN dotnet build "./EduGuide.Gateway.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EduGuide.Gateway.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EduGuide.Gateway.dll"]
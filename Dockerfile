# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["src/StorageSystem.Api/StorageSystem.Api.csproj", "src/StorageSystem.Api/"]
COPY ["src/StorageSystem.Infrastructure/StorageSystem.Infrastructure.csproj", "src/StorageSystem.Infrastructure/"]
COPY ["src/StorageSystem.Application/StorageSystem.Application.csproj", "src/StorageSystem.Application/"]
COPY ["src/StorageSystem.Domain/StorageSystem.Domain.csproj", "src/StorageSystem.Domain/"]
RUN dotnet restore "src/StorageSystem.Api/StorageSystem.Api.csproj"

# Copy the entire source code and build the application
COPY . .
WORKDIR "/src/src/StorageSystem.Api"
RUN dotnet build "StorageSystem.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish Stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "StorageSystem.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Set environment variable to configure ASP.NET to use port 8888
ENV ASPNETCORE_URLS="http://+:8888"

# Expose port 8888
EXPOSE 8888
RUN sed -i 's/TLSv1.2/TLSv1.0/g' /etc/ssl/openssl.cnf

# Copy published files and set entrypoint
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StorageSystem.Api.dll"]

# Sử dụng hình ảnh .NET Core SDK để xây dựng ứng dụng
# FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# WORKDIR /source

# # copy csproj and restore as distinct layers
# #COPY ./*.csproj .
# #RUN dotnet restore

# # copy everything else and build app
# COPY . .
# RUN dotnet publish -c release -o /app


# # final stage/image
# FROM mcr.microsoft.com/dotnet/aspnet:8.0
# WORKDIR /app

# # Set environment variable to configure ASP.NET to use port 8888
# ENV ASPNETCORE_URLS="http://+:8888"

# # Expose port 8888
# EXPOSE 8888
# COPY --from=build /app .
# ENTRYPOINT ["dotnet", "StorageSystem.WebAPI.dll"]
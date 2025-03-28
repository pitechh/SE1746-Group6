# Sử dụng image cơ bản từ Microsoft cho runtime ASP.NET Core 7.0
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 7099

# Sử dụng SDK image để build ứng dụng
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy và restore cùng một bước để tận dụng cache Docker tốt hơn
COPY WebAPI/WebAPI.csproj WebAPI/
COPY Core/Core.csproj Core/
RUN dotnet restore WebAPI/WebAPI.csproj

# Copy toàn bộ mã nguồn của WebAPI và Core
COPY . .

# Build và publish trong một bước để tối ưu
WORKDIR /src/WebAPI
RUN dotnet publish WebAPI.csproj -c Release -o /app/publish

# Tạo image cuối cùng
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# Cấu hình môi trường
ENV ASPNETCORE_URLS=http://+:7099
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ConnectionStrings__DefaultConnection="Server=host.docker.internal;Initial Catalog=HumanResourceDB;User Id=sa;Password=123;TrustServerCertificate=True;"

ENTRYPOINT ["dotnet", "WebAPI.dll"]
# ---------------- Build Stage ----------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the sln file
COPY *.sln .

# Copy all csproj files for restore
COPY Revv.Cars.Web/Revv.Cars.Web.csproj Revv.Cars.Web/
COPY Revv.Cars.DomainService/Revv.Cars.DomainService.csproj Revv.Cars.DomainService/
COPY Revv.Cars.Domain/Revv.Cars.Domain.csproj Revv.Cars.Domain/
COPY Revv.Cars.Shared/Revv.Cars.Shared.csproj Revv.Cars.Shared/

# Restore dependencies
RUN dotnet restore

# Copy everything else
COPY . .

# Build and publish
WORKDIR /src/Revv.Cars.Web
RUN dotnet publish -c Release -o /app/publish

# ---------------- Runtime Stage ----------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Expose the port (adjust if needed)
EXPOSE 80

# Set the entry point
ENTRYPOINT ["dotnet", "Revv.Cars.Web.dll"]

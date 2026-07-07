# ----- Build stage -----
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy just the project file first so `dotnet restore` is cached as its own layer
# and doesn't re-run on every source change.
COPY ConstructionExpenseManager.Api.csproj .
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o /app --no-restore

# ----- Runtime stage -----
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app .

# Render injects PORT at container start and routes traffic to it; Program.cs reads it
# via Environment.GetEnvironmentVariable("PORT") and binds Kestrel to 0.0.0.0:$PORT.
# ASPNETCORE_URLS below is only the fallback used when PORT isn't set (e.g. running this
# image locally with `docker run -p 8080:8080`, no -e PORT).
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "ConstructionExpenseManager.Api.dll"]

cd cqrs/Cqrs.Simple
set ASPNETCORE_ENVIRONMENT=Development
dotnet run --migrate-db
dotnet run --server.urls=http://localhost:8001/
PAUSE
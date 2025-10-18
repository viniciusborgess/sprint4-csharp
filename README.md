# Guardian – Redirecionamento Inteligente de Pix para Apostas

API em C# (.NET 8) com Swagger, Entity Framework Core, consultas LINQ, integrações externas (BCB Selic)
e diagramas. Inclui Dockerfile e instruções de deploy.

## Rodando localmente
```bash
dotnet build src/Guardian.Api/Guardian.Api.csproj
dotnet run --project src/Guardian.Api/Guardian.Api.csproj
# Swagger: http://localhost:5000/swagger ou https://localhost:5001/swagger
```

## Migrações (opcional)
```bash
dotnet ef migrations add InitialCreate -p src/Guardian.Api/Guardian.Api.csproj -s src/Guardian.Api/Guardian.Api.csproj
dotnet ef database update -p src/Guardian.Api/Guardian.Api.csproj -s src/Guardian.Api/Guardian.Api.csproj
```


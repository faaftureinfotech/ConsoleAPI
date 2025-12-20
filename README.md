# Backend - ConstructionFinance.API

## Quick start

1. Update `appsettings.json` connection string (MySQL).
2. Run migrations:
   dotnet ef migrations add Init
   dotnet ef database update
3. Run:
   dotnet run

## Notes
- JWT seed user: admin / admin
- Uploads currently not implemented; placeholder S3 service can be added.

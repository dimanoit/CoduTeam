dotnet ef migrations add AddTechnologiesToUser --context ApplicationDbContext --startup-project ../Web

dotnet ef database update --context ApplicationDbContext --startup-project ../Web

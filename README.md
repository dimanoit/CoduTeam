dotnet ef migrations add AddDeadlineAndStatusToPosition --context ApplicationDbContext --startup-project ../Web

dotnet ef database update --context ApplicationDbContext --startup-project ../Web

dotnet ef migrations remove --context ApplicationDbContext --startup-project ../Web
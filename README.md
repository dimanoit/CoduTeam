dotnet ef migrations add MakePositionDeadlineOptional --context ApplicationDbContext --startup-project ../Api

dotnet ef database update --context ApplicationDbContext --startup-project ../Api

dotnet ef migrations remove --context ApplicationDbContext --startup-project ../Api
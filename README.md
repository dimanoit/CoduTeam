dotnet ef migrations add IntroducePositionCategory --context ApplicationDbContext --startup-project ../Web

dotnet ef database update --context ApplicationDbContext --startup-project ../Web

dotnet ef migrations remove --context ApplicationDbContext --startup-project ../Web
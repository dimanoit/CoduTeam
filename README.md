dotnet ef migrations add UpdateMaxCharacterLimitsForPositionsAndProjects --context ApplicationDbContext --startup-project ../Web

dotnet ef database update --context ApplicationDbContext --startup-project ../Web

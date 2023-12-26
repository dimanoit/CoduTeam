dotnet ef migrations add AddImgSrcToUser --context ApplicationDbContext --startup-project ../Web

dotnet ef database update --context ApplicationDbContext --startup-project ../Web

using CoduTeam.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoduTeam.Infrastructure.Data.Configurations;

public class ChatConfiguration: IEntityTypeConfiguration<Chat>
{
    public void Configure(EntityTypeBuilder<Chat> builder)
    {
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id)
            .ValueGeneratedOnAdd();
        
        builder.HasMany<Message>(a => a.Messages)
            .WithOne(a=>a.Chat)
            .HasForeignKey(a => a.ChatId);
        
        builder.HasMany<UserChat>(a=>a.UserChats)
            .WithOne(a=>a.Chat)
            .HasForeignKey(a => a.ChatId);
        
    }
}

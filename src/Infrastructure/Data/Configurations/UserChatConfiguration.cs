using CoduTeam.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoduTeam.Infrastructure.Data.Configurations;

public class UserChatConfiguration : IEntityTypeConfiguration<UserChat>
{
    public void Configure(EntityTypeBuilder<UserChat> builder)
    {
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id)
            .ValueGeneratedOnAdd();
        builder.HasIndex(i => new { i.UserId, i.ChatId })
            .IsUnique();

        builder.HasOne(a => a.ApplicationUser)
            .WithMany(a => a.UserChats)
            .HasForeignKey(a => a.UserId);

        builder.HasOne(a => a.Chat)
            .WithMany(a => a.UserChats)
            .HasForeignKey(a => a.ChatId);
    }
}

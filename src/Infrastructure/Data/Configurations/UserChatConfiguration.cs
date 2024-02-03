using CoduTeam.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoduTeam.Infrastructure.Data.Configurations;

public class UserChatConfiguration : IEntityTypeConfiguration<UserChat>
{
    public void Configure(EntityTypeBuilder<UserChat> builder)
    {
        builder.HasKey(ap => new { ap.UserId, ap.ChatId });

        builder.HasOne(a => a.ApplicationUser)
            .WithMany(a => a.UserChats)
            .HasForeignKey(a => a.UserId);
    }
}

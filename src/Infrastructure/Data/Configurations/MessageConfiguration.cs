using CoduTeam.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoduTeam.Infrastructure.Data.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id)
            .ValueGeneratedOnAdd();

        builder.Property(c => c.Content)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.HasOne(a => a.Sender)
            .WithOne()
            .HasForeignKey<Message>(a => a.SenderId);

        builder.HasOne(a => a.Chat)
            .WithMany()
            .HasForeignKey(a => a.ChatId);

    }
}

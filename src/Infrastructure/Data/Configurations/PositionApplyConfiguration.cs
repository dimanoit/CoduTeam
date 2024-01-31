using CoduTeam.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoduTeam.Infrastructure.Data.Configurations;

public class PositionApplyConfiguration : IEntityTypeConfiguration<PositionApply>
{
    public void Configure(EntityTypeBuilder<PositionApply> builder)
    {
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id)
            .ValueGeneratedOnAdd();

        builder
            .HasIndex(p => new { p.UserId, p.PositionId })
            .IsUnique();

        builder
            .HasOne(pa => pa.User)
            .WithMany(u => u.PositionApplies)
            .HasForeignKey(pa => pa.UserId);

        builder
            .HasOne(pa => pa.Position)
            .WithMany(u => u.PositionApplies)
            .HasForeignKey(pa => pa.PositionId);
    }
}

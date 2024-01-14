using CoduTeam.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoduTeam.Infrastructure.Data.Configurations;

public class PositionConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id)
            .ValueGeneratedOnAdd();

        builder.Property(t => t.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(d => d.ShortDescription)
            .IsRequired()
            .HasMaxLength(500);
        
        builder.Property(d => d.Description)
            .IsRequired()
            .HasMaxLength(3000);

        builder.HasOne(a => a.Project)
            .WithMany(a => a.Positions)
            .HasForeignKey(a => a.ProjectId);
    }
}

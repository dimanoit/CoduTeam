using CoduTeam.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoduTeam.Infrastructure.Data.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id)
            .ValueGeneratedOnAdd();

        builder.Property(t => t.Title)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(d => d.Description)
            .IsRequired()
            .HasMaxLength(255);

        builder.HasMany(a => a.UserProjects)
            .WithOne(a => a.Project)
            .HasForeignKey(a => a.ProjectId);
    }
}

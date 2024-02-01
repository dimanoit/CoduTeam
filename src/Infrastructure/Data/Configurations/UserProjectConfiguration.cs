using CoduTeam.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoduTeam.Infrastructure.Data.Configurations;

public class UserProjectConfiguration : IEntityTypeConfiguration<UserProject>
{
    public void Configure(EntityTypeBuilder<UserProject> builder)
    {
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id)
            .ValueGeneratedOnAdd();

        builder
            .HasIndex(p => new { p.UserId, p.ProjectId })
            .IsUnique();

        builder.HasOne(a => a.ApplicationUser)
            .WithMany(a => a.UserProjects)
            .HasForeignKey(up => up.UserId);

        builder.HasOne(a => a.Project)
            .WithMany(a => a.UserProjects)
            .HasForeignKey(up => up.ProjectId);
    }
}

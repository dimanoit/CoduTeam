using CoduTeam.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoduTeam.Infrastructure.Data.Configurations;

public class UserProjectConfiguration : IEntityTypeConfiguration<UserProject>
{
    public void Configure(EntityTypeBuilder<UserProject> builder)
    {
        builder.HasKey(ap => new { ap.UserId, ap.ProjectId });

        builder.HasOne(a => a.ApplicationUser)
            .WithMany(a => a.UserProjects)
            .HasForeignKey(up => up.UserId);
    }
}

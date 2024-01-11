using CoduTeam.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoduTeam.Infrastructure.Data.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(u => u.Technologies)
            .HasConversion(
                v => string.Join(',', v),  
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)); 
    }
}

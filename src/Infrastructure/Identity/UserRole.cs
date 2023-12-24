using Microsoft.AspNetCore.Identity;

namespace CoduTeam.Infrastructure.Identity;

public class UserRole : IdentityRole<int>
{
    public UserRole()
    {
    }

    public UserRole(string roleName) : base(roleName)
    {
    }
}

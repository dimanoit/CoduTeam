using CoduTeam.Application.Common.Models;

namespace CoduTeam.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<string?> GetUserNameAsync(int userId);

    Task<bool> IsInRoleAsync(int userId, string role);

    Task<bool> AuthorizeAsync(int userId, string policyName);

    Task<(Result Result, int UserId)> CreateUserAsync(string userName, string password);

    Task<Result> DeleteUserAsync(int userId);
}

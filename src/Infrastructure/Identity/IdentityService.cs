using System.Security.Claims;
using CoduTeam.Application.Common.Exceptions;
using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Application.Common.Models;
using CoduTeam.Application.Users;
using CoduTeam.Application.Users.Command;
using CoduTeam.Application.Users.Models;
using CoduTeam.Domain.Common;
using CoduTeam.Domain.Entities;
using CoduTeam.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CoduTeam.Infrastructure.Identity;

public class IdentityService(
    UserManager<ApplicationUser> userManager,
    IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
    IAuthorizationService authorizationService,
    IUser user)
    : IIdentityService
{
    public async Task<string?> GetUserNameAsync(int userId)
    {
        ApplicationUser dbUser = await userManager.Users.FirstAsync(u => u.Id == userId);

        return dbUser.UserName;
    }

    public async Task<UserDto> GetUserDtoAsync(int userId)
    {
        ApplicationUser dbUser = await userManager.Users
            .FirstAsync(u => u.Id == userId);

        return dbUser.ToUserDto();
    }

    public async Task<(Result Result, int UserId)> CreateUserAsync(string userName, string password)
    {
        ApplicationUser dbUser = new() { UserName = userName, Email = userName };

        IdentityResult result = await userManager.CreateAsync(dbUser, password);

        return (result.ToApplicationResult(), dbUser.Id);
    }

    public async Task ActivateUserAsync(ActivationUserCommand userDto)
    {
        Guard.Against.Null(user.Id);

        ApplicationUser userToActivate = await userManager.Users
            .FirstAsync(u => u.Id == user.Id);
        userToActivate.MapUser(userDto);
        await userManager.UpdateAsync(userToActivate);
    }

    public void ThrowIfNoAccessToResource(BaseAuditableEntity resource)
    {
        if (user.Id != resource.CreatedBy)
        {
            throw new ForbiddenAccessException();
        }
    }

    public async Task<bool> IsInRoleAsync(int userId, string role)
    {
        ApplicationUser? dbUser = userManager.Users.SingleOrDefault(u => u.Id == userId);

        return dbUser != null && await userManager.IsInRoleAsync(dbUser, role);
    }

    public async Task<bool> AuthorizeAsync(int userId, string policyName)
    {
        ApplicationUser? dbUser = userManager.Users.SingleOrDefault(u => u.Id == userId);

        if (dbUser == null)
        {
            return false;
        }

        ClaimsPrincipal principal = await userClaimsPrincipalFactory.CreateAsync(dbUser);

        AuthorizationResult result = await authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    public async Task<Result> DeleteUserAsync(int userId)
    {
        ApplicationUser? dbUser = userManager.Users.SingleOrDefault(u => u.Id == userId);

        return dbUser != null ? await DeleteUserAsync(dbUser) : Result.Success();
    }

    public async Task<Result> DeleteUserAsync(ApplicationUser dbUser)
    {
        IdentityResult result = await userManager.DeleteAsync(dbUser);

        return result.ToApplicationResult();
    }
}

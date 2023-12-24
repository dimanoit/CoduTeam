using System.Security.Claims;
using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Application.Common.Models;
using CoduTeam.Application.Users.Command;
using CoduTeam.Application.Users.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CoduTeam.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly IAuthorizationService _authorizationService;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
    private readonly UserManager<ApplicationUser> _userManager;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService)
    {
        _userManager = userManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _authorizationService = authorizationService;
    }

    public async Task<string?> GetUserNameAsync(int userId)
    {
        ApplicationUser user = await _userManager.Users.FirstAsync(u => u.Id == userId);

        return user.UserName;
    }

    public async Task<UserDto> GetUserDtoAsync(int userId)
    {
        ApplicationUser user = await _userManager.Users
            .FirstAsync(u => u.Id == userId);

        return user.ToUserDto();
    }

    public async Task<(Result Result, int UserId)> CreateUserAsync(string userName, string password)
    {
        ApplicationUser user = new() { UserName = userName, Email = userName };

        IdentityResult result = await _userManager.CreateAsync(user, password);

        return (result.ToApplicationResult(), user.Id);
    }

    public async Task ActivateUserAsync(ActivationUserCommand userDto)
    {
        ApplicationUser user = await _userManager.Users
            .FirstAsync(u => u.Id == userDto.UserId);

        user.FirstName = userDto.FirstName;
        user.LastName = userDto.LastName;
        user.DateOfBirth = userDto.DateOfBirth;
        user.Gender = userDto.Gender;
        user.Title = userDto.Title;
        user.UserStatus = UserStatus.Active;

        await _userManager.UpdateAsync(user);
    }

    public async Task<bool> IsInRoleAsync(int userId, string role)
    {
        ApplicationUser? user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<bool> AuthorizeAsync(int userId, string policyName)
    {
        ApplicationUser? user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        if (user == null)
        {
            return false;
        }

        ClaimsPrincipal principal = await _userClaimsPrincipalFactory.CreateAsync(user);

        AuthorizationResult result = await _authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    public async Task<Result> DeleteUserAsync(int userId)
    {
        ApplicationUser? user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        return user != null ? await DeleteUserAsync(user) : Result.Success();
    }

    public async Task<Result> DeleteUserAsync(ApplicationUser user)
    {
        IdentityResult result = await _userManager.DeleteAsync(user);

        return result.ToApplicationResult();
    }
}

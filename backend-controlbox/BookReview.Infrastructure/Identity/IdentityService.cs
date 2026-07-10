using BookReview.Application.Common.Interfaces.Authentication;
using Microsoft.AspNetCore.Identity;

namespace BookReview.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public IdentityService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<(bool IsSuccess, string UserId, string Error)> RegisterUserAsync(string email, string userName, string password)
    {
        var user = new ApplicationUser { UserName = userName, Email = email };
        var result = await _userManager.CreateAsync(user, password);

        return result.Succeeded 
            ? (true, user.Id, string.Empty) 
            : (false, string.Empty, string.Join(", ", result.Errors.Select(e => e.Description)));
    }

    public async Task<bool> CheckPasswordAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user != null && await _userManager.CheckPasswordAsync(user, password);
    }

    public async Task<(string UserId, string UserName)> GetUserDetailsAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return (user!.Id, user.UserName!);
    }

    public async Task<string> GetUserNameAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        return user?.UserName ?? "Usuario Desconocido";
    }
}
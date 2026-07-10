using BookReview.Application.Common.Interfaces.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

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
        return user?.UserName ?? "Unknown";
    }

    public async Task<string> GetProfilePictureAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        return user?.ProfilePictureUrl ?? string.Empty;
    }

    public async Task<bool> UpdateProfilePictureAsync(string userId, string pictureUrl)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return false;
        
        user.ProfilePictureUrl = pictureUrl;
        var result = await _userManager.UpdateAsync(user);
        return result.Succeeded;
    }

    public async Task<string> GeneratePasswordResetTokenAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null) return string.Empty;
        
        return await _userManager.GeneratePasswordResetTokenAsync(user);
    }

    public async Task<bool> ResetPasswordAsync(string email, string token, string newPassword)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null) return false;
        
        var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
        return result.Succeeded;
    }
}
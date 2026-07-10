using System.Threading.Tasks;

namespace BookReview.Application.Common.Interfaces.Authentication;

public interface IIdentityService
{
    Task<(bool IsSuccess, string UserId, string Error)> RegisterUserAsync(string email, string userName, string password);
    Task<bool> CheckPasswordAsync(string email, string password);
    Task<(string UserId, string UserName)> GetUserDetailsAsync(string email);
    Task<string> GetUserNameAsync(string userId);
    Task<string> GetProfilePictureAsync(string userId);
    Task<bool> UpdateProfilePictureAsync(string userId, string pictureUrl);
    Task<string> GeneratePasswordResetTokenAsync(string email);
    Task<bool> ResetPasswordAsync(string email, string token, string newPassword);
}
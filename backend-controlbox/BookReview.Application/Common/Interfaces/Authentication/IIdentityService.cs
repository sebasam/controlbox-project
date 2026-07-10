namespace BookReview.Application.Common.Interfaces.Authentication;

public interface IIdentityService
{
    Task<(bool IsSuccess, string UserId, string Error)> RegisterUserAsync(string email, string userName, string password);
    Task<bool> CheckPasswordAsync(string email, string password);
    Task<(string UserId, string UserName)> GetUserDetailsAsync(string email);
    Task<string> GetUserNameAsync(string userId);
}
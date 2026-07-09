namespace BookReview.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(string userId, string email, string userName);
}
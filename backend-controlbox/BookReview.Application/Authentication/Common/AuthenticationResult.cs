namespace BookReview.Application.Authentication.Common;

public record AuthenticationResult(
    string Id, 
    string Email, 
    string UserName, 
    string Token
);
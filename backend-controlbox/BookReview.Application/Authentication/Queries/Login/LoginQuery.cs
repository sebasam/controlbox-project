using BookReview.Application.Authentication.Common;
using MediatR;

namespace BookReview.Application.Authentication.Queries.Login;

public record LoginQuery(
    string Email, 
    string Password
) : IRequest<AuthenticationResult>;
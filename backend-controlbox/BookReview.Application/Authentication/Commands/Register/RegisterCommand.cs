using BookReview.Application.Authentication.Common;
using MediatR;

namespace BookReview.Application.Authentication.Commands.Register;

public record RegisterCommand(
    string Email, 
    string UserName, 
    string Password
) : IRequest<AuthenticationResult>;
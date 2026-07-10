using BookReview.Application.Common.Interfaces.Authentication;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookReview.Application.Authentication.Commands.ForgotPassword;

public record ForgotPasswordCommand(string Email) : IRequest<string>;

public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, string>
{
    private readonly IIdentityService _identityService;

    public ForgotPasswordCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<string> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var token = await _identityService.GeneratePasswordResetTokenAsync(request.Email);
        if (string.IsNullOrEmpty(token)) throw new Exception("User not found");
        return token;
    }
}
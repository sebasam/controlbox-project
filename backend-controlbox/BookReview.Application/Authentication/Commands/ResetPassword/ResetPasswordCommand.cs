using BookReview.Application.Common.Interfaces.Authentication;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookReview.Application.Authentication.Commands.ResetPassword;

public record ResetPasswordCommand(string Email, string Token, string NewPassword) : IRequest<bool>;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, bool>
{
    private readonly IIdentityService _identityService;

    public ResetPasswordCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var result = await _identityService.ResetPasswordAsync(request.Email, request.Token, request.NewPassword);
        if (!result) throw new Exception("Invalid token or email");
        return result;
    }
}
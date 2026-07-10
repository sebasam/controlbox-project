using BookReview.Application.Authentication.Common;
using BookReview.Application.Common.Interfaces.Authentication;
using MediatR;

namespace BookReview.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthenticationResult>
{
    private readonly IIdentityService _identityService;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public RegisterCommandHandler(IIdentityService identityService, IJwtTokenGenerator jwtTokenGenerator)
    {
        _identityService = identityService;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<AuthenticationResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var result = await _identityService.RegisterUserAsync(request.Email, request.UserName, request.Password);
        
        if (!result.IsSuccess)
            throw new Exception(result.Error);

        var token = _jwtTokenGenerator.GenerateToken(result.UserId, request.Email, request.UserName);

        return new AuthenticationResult(result.UserId, request.Email, request.UserName, token);
    }
}
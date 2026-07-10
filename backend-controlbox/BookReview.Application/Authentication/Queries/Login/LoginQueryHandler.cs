using BookReview.Application.Authentication.Common;
using BookReview.Application.Common.Interfaces.Authentication;
using MediatR;

namespace BookReview.Application.Authentication.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthenticationResult>
{
    private readonly IIdentityService _identityService;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginQueryHandler(IIdentityService identityService, IJwtTokenGenerator jwtTokenGenerator)
    {
        _identityService = identityService;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<AuthenticationResult> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var isValid = await _identityService.CheckPasswordAsync(request.Email, request.Password);

        if (!isValid)
            throw new Exception("Invalid credentials");

        var userDetails = await _identityService.GetUserDetailsAsync(request.Email);
        var token = _jwtTokenGenerator.GenerateToken(userDetails.UserId, request.Email, userDetails.UserName);

        return new AuthenticationResult(userDetails.UserId, request.Email, userDetails.UserName, token);
    }
}
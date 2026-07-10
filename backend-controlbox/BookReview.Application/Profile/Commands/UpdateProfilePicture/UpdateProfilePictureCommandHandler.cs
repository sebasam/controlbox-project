using BookReview.Application.Common.Interfaces.Authentication;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BookReview.Application.Profile.Commands.UpdateProfilePicture;

public class UpdateProfilePictureCommandHandler : IRequestHandler<UpdateProfilePictureCommand, bool>
{
    private readonly IIdentityService _identityService;

    public UpdateProfilePictureCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<bool> Handle(UpdateProfilePictureCommand request, CancellationToken cancellationToken)
    {
        return await _identityService.UpdateProfilePictureAsync(request.UserId, request.PictureUrl);
    }
}
using MediatR;

namespace BookReview.Application.Profile.Commands.UpdateProfilePicture;

public record UpdateProfilePictureCommand(string UserId, string PictureUrl) : IRequest<bool>;
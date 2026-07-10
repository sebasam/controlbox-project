using MediatR;
using System;
using System.Collections.Generic;

namespace BookReview.Application.Profile.Queries.GetProfile;

public record UserReviewDto(Guid Id, Guid BookId, string BookTitle, int Rating, string Comment, DateTime CreatedAt);
public record ProfileDto(string UserName, string Email, string ProfilePictureUrl, List<UserReviewDto> Reviews);

public record GetProfileQuery(string UserId, string Email) : IRequest<ProfileDto>;
using BookReview.Application.Profile.Commands.UpdateProfilePicture;
using BookReview.Application.Profile.Queries.GetProfile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookReview.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProfileController : ControllerBase
{
    private readonly ISender _mediator;

    public ProfileController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetProfile()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var email = User.FindFirstValue(ClaimTypes.Email);
        
        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(email)) return Unauthorized();

        var result = await _mediator.Send(new GetProfileQuery(userId, email));
        return Ok(result);
    }

    [HttpPut("picture")]
    public async Task<IActionResult> UpdatePicture([FromBody] UpdatePictureDto request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        await _mediator.Send(new UpdateProfilePictureCommand(userId, request.PictureUrl));
        return NoContent();
    }
}

public record UpdatePictureDto(string PictureUrl);
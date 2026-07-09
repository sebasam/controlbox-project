using Microsoft.AspNetCore.Identity;

namespace BookReview.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string ProfilePictureUrl { get; set; } = string.Empty;
}
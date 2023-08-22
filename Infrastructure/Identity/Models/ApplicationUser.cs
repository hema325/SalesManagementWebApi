using Infrastructure.Authentication.Models;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Models
{
    internal class ApplicationUser: IdentityUser<string>
    {
        public List<RefreshToken> RefreshTokens { get; set; }
    }
}

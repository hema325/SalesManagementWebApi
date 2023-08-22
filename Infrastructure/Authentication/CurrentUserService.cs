using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Infrastructure.Authentication
{
    internal class CurrentUserService : ICurrentUser
    {
        private readonly HttpContext _context;

        public CurrentUserService(IHttpContextAccessor accessor)
        {
            _context = accessor.HttpContext!;
        }

        public string Id => _context.User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        public string UserName => _context.User?.FindFirstValue(ClaimTypes.Name)!;
    }
}

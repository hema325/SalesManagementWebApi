using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    internal class IdentityService: IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> IsInRoleAsync(string id, Roles role)
        {
            var user = await _userManager.FindByIdAsync(id);
            return await _userManager.IsInRoleAsync(user, role.ToString());
        }

        public async Task AddToRoleAsync(string id, Roles role)
        {
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.AddToRoleAsync(user, role.ToString()); 
        }

        public async Task AddToRolesAsync(string id, IEnumerable<Roles> roles)
        {
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.AddToRolesAsync(user, roles.Select(r => r.ToString()));
        }

        public async Task ChangePasswordAsync(string id, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        }

        public async Task<bool> IsUserNameExsitsAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName) != null;
        } 
    }
}

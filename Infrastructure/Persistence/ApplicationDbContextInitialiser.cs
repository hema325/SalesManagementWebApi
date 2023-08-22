using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    internal class ApplicationDbContextInitialiser
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationDbContextInitialiser(ApplicationDbContext context,
                                               RoleManager<ApplicationRole> roleManager,
                                               UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task InitialiseAsync()
        {
            await _context.Database.MigrateAsync();
            await Seed();
        }

        private async Task Seed()
        {
            // seeding our identity
            var roles = Enum.GetValues<Roles>()
                            .Select(r => new ApplicationRole { Name = r.ToString() });

            var users = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    UserName = "Admin"
                }
            };

            if (await _roleManager.FindByNameAsync(Roles.Admin.ToString()) == null)
            {
                foreach (var role in roles)
                    await _roleManager.CreateAsync(role);
            }

            if (await _userManager.FindByNameAsync(users[0].UserName) == null)
            {
                await Task.WhenAll(users.Select(u => _userManager.CreateAsync(u, "P@2sword")));
                await Task.WhenAll(users.Select(u => _userManager.AddToRoleAsync(u, Roles.Admin.ToString())));
            }

        }
    }
}

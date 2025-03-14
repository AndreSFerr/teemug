using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using TeeMugShop.Domain.Entities.Application;

namespace TeeMugShop.Infrastructure.Persistence
{
    public class ApplicationDbContextInitialiser
    {
        private const string AdminDefaultUserName = "admin";
        private const string AdminDefaultEmail = "administrator@localhost";
        private const string AdminDefaultFullName = "Administrator";
        private const string AdminDefaultPassword = "123.abc.ABC";

        private readonly ILogger<ApplicationDbContextInitialiser> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public ApplicationDbContextInitialiser(
            ILogger<ApplicationDbContextInitialiser> logger,
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                await _context.Database.EnsureCreatedAsync();
                await SeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during database initialization.");
                throw;
            }
        }

        private async Task SeedAsync()
        {
            try
            {
                await SeedRolesAsync();
                await SeedAdminUserAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        private async Task SeedRolesAsync()
        {
            var roles = new[] { "Admin", "User" };

            foreach (var roleName in roles)
            {
                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    // 👇 
                    await _roleManager.CreateAsync(new ApplicationRole
                    {
                        Name = roleName,
                        NormalizedName = roleName.ToUpperInvariant()
                    });
                }
            }
        }


        private async Task SeedAdminUserAsync()
        {
            var adminUser = await _userManager.FindByNameAsync(AdminDefaultUserName);

            if (adminUser == null)
            {
                var user = new ApplicationUser
                {
                    UserName = AdminDefaultUserName,
                    Email = AdminDefaultEmail,
                    FullName = AdminDefaultFullName,
                    EmailConfirmed = true,
                    IsActive = true
                };

                var createResult = await _userManager.CreateAsync(user, AdminDefaultPassword);

                if (createResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                }
                else
                {
                    _logger.LogError("Failed to create admin user: {errors}", string.Join(", ", createResult.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}

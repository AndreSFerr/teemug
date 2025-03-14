using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using TeeMugShop.Domain.Entities.Application;

namespace TeeMugShop.Infrastructure.Persistence
{
    public class ApplicationDbContextInitialiser
    {
        private const string AdminDevelopmentPassword = "123.abc.ABC";
        private const string AdminDevelopmentUsername = "admin";
        private const string AdminDevelopmentEmail = "administrator@localhost";
        private const string AdminDevelopmentName = "Admin";

        private readonly ILogger<ApplicationDbContextInitialiser> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public ApplicationDbContextInitialiser(
            ILogger<ApplicationDbContextInitialiser> logger,
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole<Guid>> roleManager)
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

        public async Task SeedAsync()
        {
            try
            {
                await SeedRolesAsync();
                await SeedAdministratorUserAsync();
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
                    await _roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
                }
            }
        }

        private async Task SeedAdministratorUserAsync()
        {
            var adminUser = await _userManager.FindByNameAsync(AdminDevelopmentUsername);

            if (adminUser == null)
            {
                var admin = new ApplicationUser
                {
                    UserName = AdminDevelopmentUsername,
                    Email = AdminDevelopmentEmail,
                    FullName = AdminDevelopmentName,
                    EmailConfirmed = true,
                    IsActive = true
                };

                var result = await _userManager.CreateAsync(admin, AdminDevelopmentPassword);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}

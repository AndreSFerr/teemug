using Microsoft.AspNetCore.Identity;

namespace TeeMugShop.Domain.Entities.Application
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? NIF { get; set; }
        public string? Phone { get; set; }
        public bool IsActive { get; set; }
    }
}

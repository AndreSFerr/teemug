using Microsoft.AspNetCore.Identity;


namespace TeeMugShop.Domain.Entities.Application
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole()
        {
            Roles = new List<ApplicationUserRole>();
        }

        public ApplicationRole(string roleName)
            : base(roleName)
        {
            Roles = new List<ApplicationUserRole>();
        }

        public ICollection<ApplicationUserRole> Roles { get; set; }
    }
}

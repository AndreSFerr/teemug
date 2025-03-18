
using TeeMugShop.Domain.Entities.Application;

namespace TeeMugShop.Application.Common.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(ApplicationUser user);
    }
}

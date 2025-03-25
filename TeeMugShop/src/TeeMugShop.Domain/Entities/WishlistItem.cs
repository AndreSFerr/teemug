using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeeMugShop.Domain.Entities.Application;

namespace TeeMugShop.Domain.Entities
{
    public class WishlistItem
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public ApplicationUser User { get; set; } = default!;
        public Product Product { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}

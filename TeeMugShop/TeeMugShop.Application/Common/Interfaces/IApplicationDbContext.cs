using Microsoft.EntityFrameworkCore;
using TeeMugShop.Domain.Entities;
using TeeMugShop.Domain.Entities.Application;

namespace TeeMugShop.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Product> Products { get; }
        DbSet<Order> Orders { get; }
        DbSet<Cart> Carts { get; }
        DbSet<CartItem> CartItems { get; }
        DbSet<OrderItem> OrderItems { get; }
        DbSet<ApplicationRole> ApplicationRoles { get; }


        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}

using MediatR;
using TeeMugShop.Domain.Entities;

namespace TeeMugShop.Application.Carts.Queries
{
    public class GetCartByIdQuery : IRequest<Cart?>
    {
        public Guid CartId { get; set; }
    }
}
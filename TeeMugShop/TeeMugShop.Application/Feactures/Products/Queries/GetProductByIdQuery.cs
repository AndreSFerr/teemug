using MediatR;
using TeeMugShop.Domain.Entities;

namespace TeeMugShop.Application.Feactures.Products.Queries
{
    public class GetProductByIdQuery : IRequest<Product?>
    {
        public Guid Id { get; set; }
    }
}

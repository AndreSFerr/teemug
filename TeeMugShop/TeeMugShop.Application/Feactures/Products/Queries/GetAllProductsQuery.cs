using MediatR;
using TeeMugShop.Domain.Entities;

namespace TeeMugShop.Application.Feactures.Products.Queries
{
    public class GetAllProductsQuery : IRequest<List<Product>> { }
}

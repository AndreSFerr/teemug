using MediatR;
using TeeMugShop.Domain.Entities;

namespace TeeMugShop.Application.Carts.Queries
{
    public class GetAllCartsQuery : IRequest<List<Cart>> { }
}
using MediatR;
using TeeMugShop.Application.DTOs;

namespace TeeMugShop.Application.Feactures.Admin.Queries
{
    public record GetAllOrdersQuery : IRequest<List<OrderDto>>;
}

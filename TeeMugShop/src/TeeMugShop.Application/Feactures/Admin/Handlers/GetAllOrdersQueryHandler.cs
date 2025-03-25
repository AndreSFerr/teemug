using MediatR;
using Microsoft.EntityFrameworkCore;
using TeeMugShop.Application.Common.Interfaces;
using TeeMugShop.Application.DTOs;
using TeeMugShop.Application.Feactures.Admin.Queries;

public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, List<OrderDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAllOrdersQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        return await _context.Orders
            .Select(o => new OrderDto
            {
                Id = o.Id,
                CustomerName = o.User.FullName!,
                TotalAmount = o.Items.Sum(i => i.Product.Price * i.Quantity),
                CreatedAt = o.CreatedAt,
                Status = o.Status.ToString()
            })
            .ToListAsync(cancellationToken);
    }
}

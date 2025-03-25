using MediatR;
using Microsoft.EntityFrameworkCore;
using TeeMugShop.Application.Common.Interfaces;
using TeeMugShop.Application.DTOs;
using TeeMugShop.Application.Feactures.Admin.Queries;

public class GetDashboardMetricsQueryHandler : IRequestHandler<GetDashboardMetricsQuery, DashboardMetricsDto>
{
    private readonly IApplicationDbContext _context;

    public GetDashboardMetricsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DashboardMetricsDto> Handle(GetDashboardMetricsQuery request, CancellationToken cancellationToken)
    {
        var totalOrders = await _context.Orders.CountAsync(cancellationToken);
        var totalRevenue = await _context.Orders
            .SelectMany(o => o.Items)
            .SumAsync(i => i.Product.Price * i.Quantity, cancellationToken);

        var ordersToday = await _context.Orders
            .CountAsync(o => o.CreatedAt.Date == DateTime.UtcNow.Date, cancellationToken);

        var ordersByStatus = await _context.Orders
            .GroupBy(o => o.Status.ToString())
            .Select(g => new { Status = g.Key, Count = g.Count() })
            .ToDictionaryAsync(x => x.Status, x => x.Count, cancellationToken);

        return new DashboardMetricsDto
        {
            TotalOrders = totalOrders,
            TotalRevenue = totalRevenue,
            OrdersToday = ordersToday,
            OrdersByStatus = ordersByStatus
        };
    }
}

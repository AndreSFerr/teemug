using MediatR;
using TeeMugShop.Application.DTOs;

namespace TeeMugShop.Application.Feactures.Admin.Queries
{    
    public record GetDashboardMetricsQuery : IRequest<DashboardMetricsDto>;
}

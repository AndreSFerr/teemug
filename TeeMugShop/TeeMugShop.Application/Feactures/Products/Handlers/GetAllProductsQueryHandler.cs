using MediatR;
using Microsoft.EntityFrameworkCore;
using TeeMugShop.Application.Common.Interfaces;
using TeeMugShop.Application.Feactures.Products.Queries;
using TeeMugShop.Domain.Entities;

namespace TeeMugShop.Application.Products.Handlers
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<Product>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllProductsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Products.ToListAsync(cancellationToken);
        }
    }
}
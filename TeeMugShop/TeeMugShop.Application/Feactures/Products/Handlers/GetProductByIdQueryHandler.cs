
using MediatR;
using TeeMugShop.Application.Common.Interfaces;
using TeeMugShop.Application.Feactures.Products.Queries;
using TeeMugShop.Domain.Entities;

namespace TeeMugShop.Application.Products.Handlers
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product?>
    {
        private readonly IApplicationDbContext _context;

        public GetProductByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Product?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Products.FindAsync(new object[] { request.Id }, cancellationToken);
        }
    }
}
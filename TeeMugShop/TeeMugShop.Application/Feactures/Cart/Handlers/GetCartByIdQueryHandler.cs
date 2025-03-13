using MediatR;
using TeeMugShop.Application.Carts.Queries;
using TeeMugShop.Application.Common.Interfaces;
using TeeMugShop.Domain.Entities;

namespace TeeMugShop.Application.Carts.Handlers
{
    public class GetCartByIdQueryHandler : IRequestHandler<GetCartByIdQuery, Cart?>
    {
        private readonly IApplicationDbContext _context;

        public GetCartByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Cart?> Handle(GetCartByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Carts.FindAsync(new object[] { request.CartId }, cancellationToken);
        }
    }
}
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeeMugShop.Application.Carts.Queries;
using TeeMugShop.Application.Common.Interfaces;
using TeeMugShop.Domain.Entities;

namespace TeeMugShop.Application.Carts.Handlers
{
    public class GetAllCartsQueryHandler : IRequestHandler<GetAllCartsQuery, List<Cart>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllCartsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Cart>> Handle(GetAllCartsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Carts.ToListAsync(cancellationToken);
        }
    }
}
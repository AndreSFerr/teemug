
using MediatR;
using TeeMugShop.Application.Carts.Commands;
using TeeMugShop.Application.Common.Interfaces;
using TeeMugShop.Domain.Entities;

namespace TeeMugShop.Application.Carts.Handlers
{
    public class UpdateCartCommandHandler : IRequestHandler<UpdateCartCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateCartCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await _context.Carts.FindAsync(new object[] { request.CartId }, cancellationToken);
            if (cart == null) return;

            cart.UserId = request.UserId;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
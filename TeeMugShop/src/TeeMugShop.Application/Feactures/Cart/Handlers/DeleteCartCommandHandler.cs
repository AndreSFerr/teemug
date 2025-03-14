
using MediatR;
using TeeMugShop.Application.Carts.Commands;
using TeeMugShop.Application.Common.Interfaces;

namespace TeeMugShop.Application.Carts.Handlers
{
    public class DeleteCartCommandHandler : IRequestHandler<DeleteCartCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteCartCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await _context.Carts.FindAsync(new object[] { request.CartId }, cancellationToken);
            if (cart == null) return;

            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

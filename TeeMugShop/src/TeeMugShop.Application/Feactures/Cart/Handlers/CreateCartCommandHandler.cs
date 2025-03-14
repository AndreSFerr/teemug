using MediatR;
using TeeMugShop.Application.Carts.Commands;
using TeeMugShop.Application.Common.Interfaces;
using TeeMugShop.Domain.Entities;

namespace TeeMugShop.Application.Carts.Handlers
{
    public class CreateCartCommandHandler : IRequestHandler<CreateCartCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public CreateCartCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            var cart = new Cart
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                CreatedAt = DateTime.UtcNow
            };

            _context.Carts.Add(cart);
            await _context.SaveChangesAsync(cancellationToken);

            return cart.Id;
        }
    }
}
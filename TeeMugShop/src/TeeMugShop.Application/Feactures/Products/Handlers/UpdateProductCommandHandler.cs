using MediatR;
using TeeMugShop.Application.Common.Interfaces;
using TeeMugShop.Application.Feactures.Products.Commands;

namespace TeeMugShop.Application.Products.Handlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateProductCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FindAsync(new object[] { request.Id }, cancellationToken);
            if (product == null) return;

            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.ImageUrl = request.ImageUrl;
            product.Category = request.Category;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

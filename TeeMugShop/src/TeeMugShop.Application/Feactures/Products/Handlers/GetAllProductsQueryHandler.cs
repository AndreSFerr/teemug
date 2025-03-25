using MediatR;
using Microsoft.EntityFrameworkCore;
using TeeMugShop.Application.Common.Interfaces;
using TeeMugShop.Application.DTOs;
using TeeMugShop.Application.Feactures.Products.Queries;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAllProductsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Products.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Name))
            query = query.Where(p => p.Name.Contains(request.Name));

        if (!string.IsNullOrWhiteSpace(request.Description))
            query = query.Where(p => p.Description.Contains(request.Description));

        if (request.Price.HasValue)
            query = query.Where(p => p.Price == request.Price.Value);

        if (request.Category.HasValue)
            query = query.Where(p => p.Category == request.Category);

        return await query
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                Category = p.Category,                
            })
            .ToListAsync(cancellationToken);
    }
}

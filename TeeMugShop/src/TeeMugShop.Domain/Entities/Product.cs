using TeeMugShop.Domain.Enums;

namespace TeeMugShop.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = default!;
        public CategoryType Category { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

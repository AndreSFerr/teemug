

namespace TeeMugShop.Application.DTOs
{
    public class SalesByCategoryDto
    {
        public string Category { get; set; } = default!;
        public int Count { get; set; }
        public decimal Total { get; set; }
    }
}

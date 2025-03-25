
namespace TeeMugShop.Application.DTOs
{
    public class SalesPerMonthDto
    {
        public string Month { get; set; } = default!;
        public int Orders { get; set; }
        public decimal Sales { get; set; }
    }
}

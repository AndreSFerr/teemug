
namespace TeeMugShop.Application.DTOs
{
    public class DashboardMetricsDto
    {
        public int TotalOrders { get; set; }     // Total geral de pedidos
        public decimal TotalRevenue { get; set; }   // Faturamento total
        public int OrdersToday { get; set; }   // Quantos pedidos foram feitos hoje
        public Dictionary<string, int> OrdersByStatus { get; set; } = new();   // Exemplo: {"Pending": 3, "Completed": 7, "Canceled": 2}
    }
}

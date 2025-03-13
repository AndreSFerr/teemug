using MediatR;

namespace TeeMugShop.Application.Carts.Commands
{
    public class UpdateCartCommand : IRequest
    {
        public Guid CartId { get; set; }
        public Guid UserId { get; set; }
    }
}


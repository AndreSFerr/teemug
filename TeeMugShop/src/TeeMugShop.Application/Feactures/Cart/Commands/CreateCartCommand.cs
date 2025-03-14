using MediatR;

namespace TeeMugShop.Application.Carts.Commands
{
    public class CreateCartCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
    }
}
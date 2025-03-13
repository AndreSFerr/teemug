// DeleteCartCommand.cs
using MediatR;

namespace TeeMugShop.Application.Carts.Commands
{
    public class DeleteCartCommand : IRequest
    {
        public Guid CartId { get; set; }
    }
}
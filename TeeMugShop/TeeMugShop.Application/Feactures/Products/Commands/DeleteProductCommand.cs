using MediatR;


namespace TeeMugShop.Application.Feactures.Products.Commands
{
    public class DeleteProductCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}

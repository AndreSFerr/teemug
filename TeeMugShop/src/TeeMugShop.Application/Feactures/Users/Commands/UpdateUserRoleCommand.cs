using MediatR;

namespace TeeMugShop.Application.Feactures.Users.Commands
{
    public record UpdateUserRoleCommand : IRequest
    {
        public Guid UserId { get; set; }
        public string NewRole { get; set; } = string.Empty;
    }
}

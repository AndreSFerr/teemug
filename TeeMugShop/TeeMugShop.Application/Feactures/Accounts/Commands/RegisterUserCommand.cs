using MediatR;

namespace TeeMugShop.Application.Features.Accounts.Commands
{
    public class RegisterUserCommand : IRequest<bool>
    {
        public string FullName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string NIF { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}

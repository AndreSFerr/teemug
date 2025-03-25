using MediatR;


namespace TeeMugShop.Application.Feactures.Accounts.Commands
{
    public class LoginUserCommand : IRequest<ExternalLoginTokenResult>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

}

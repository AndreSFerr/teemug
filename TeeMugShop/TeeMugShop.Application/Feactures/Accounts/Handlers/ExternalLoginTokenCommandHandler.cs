using MediatR;
using Microsoft.AspNetCore.Identity;
using TeeMugShop.Application.Feactures.Accounts.Commands;
using TeeMugShop.Domain.Entities.Application;

namespace TeeMugShop.Application.Features.Accounts.Commands
{
    public class ExternalLoginTokenCommandHandler : IRequestHandler<ExternalLoginTokenCommand, ExternalLoginTokenResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ExternalLoginTokenCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ExternalLoginTokenResult> Handle(ExternalLoginTokenCommand request, CancellationToken cancellationToken)
        {
            // Aqui você deveria validar o token (Google ou Facebook) com suas respectivas APIs
            // Por exemplo, chamar Google: https://oauth2.googleapis.com/tokeninfo?id_token=...

            // Simulação apenas:
            var email = "emaildoGoogleOuFacebook@example.com";
            var name = "Nome Do Usuário";

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    FullName = name
                };

                var result = await _userManager.CreateAsync(user);
                if (!result.Succeeded)
                {
                    return new ExternalLoginTokenResult
                    {
                        Success = false,
                        Message = "Erro ao criar usuário."
                    };
                }

                await _userManager.AddLoginAsync(user, new UserLoginInfo(request.Provider, request.AccessToken, request.Provider));
            }

            return new ExternalLoginTokenResult
            {
                Success = true,
                Message = "Login realizado com sucesso.",
                UserId = user.Id,
                Email = user.Email
                // Aqui poderia gerar um JWT também
            };
        }
    }
}

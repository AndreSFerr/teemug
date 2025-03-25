using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Data;
using TeeMugShop.Application.Common.Interfaces;
using TeeMugShop.Application.Common.Settings;
using TeeMugShop.Application.Feactures.Accounts.Commands;
using TeeMugShop.Domain.Entities.Application;

namespace TeeMugShop.Application.Feactures.Accounts.Handlers
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, ExternalLoginTokenResult>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly IJwtTokenService _jwtTokenService;

        public LoginUserCommandHandler(SignInManager<ApplicationUser> signInManager,
                                       UserManager<ApplicationUser> userManager,
                                       IOptions<JwtSettings> jwtOptions,
                                       IJwtTokenService jwtTokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtSettings = jwtOptions.Value;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<ExternalLoginTokenResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return new ExternalLoginTokenResult { Success = false, Message = "Usuário não encontrado." };

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)
                return new ExternalLoginTokenResult { Success = false, Message = "Senha inválida." };

            var roles = await _userManager.GetRolesAsync(user);

            var token = _jwtTokenService.GenerateToken(user);

            return new ExternalLoginTokenResult
            {
                Success = true,
                Token = token,
                User = new
                {
                    Name = user.FullName,
                    Email = user.Email!,
                    Picture = "",
                    Role = roles.FirstOrDefault() ?? ""
                }
            };
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Text.Json;
using TeeMugShop.Application.Common.Settings;
using TeeMugShop.Application.DTOs.Auth;
using TeeMugShop.Domain.Entities.Application;
using TeeMugShop.Application.Feactures.Accounts.Commands;
using TeeMugShop.Application.Common.Interfaces;

namespace TeeMugShop.Application.Features.Accounts.Commands
{
    public class ExternalLoginTokenCommandHandler : IRequestHandler<ExternalLoginTokenCommand, ExternalLoginTokenResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly IJwtTokenService _jwtTokenService;

        public ExternalLoginTokenCommandHandler(UserManager<ApplicationUser> userManager, IOptions<JwtSettings> jwtOptions, IJwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _jwtSettings = jwtOptions.Value;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<ExternalLoginTokenResult> Handle(ExternalLoginTokenCommand request, CancellationToken cancellationToken)
        {
            string? email = null;
            string? name = null;
            string? picture = null;

            using var client = new HttpClient();

            if (request.Provider.ToLower() == "google")
            {
                var validationUrl = $"https://oauth2.googleapis.com/tokeninfo?id_token={request.Token}";
                var response = await client.GetAsync(validationUrl);
                if (!response.IsSuccessStatusCode) return new ExternalLoginTokenResult { Success = false };

                var content = await response.Content.ReadAsStringAsync();
                var payload = JsonSerializer.Deserialize<GoogleTokenPayload>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (payload == null || string.IsNullOrEmpty(payload.Email))
                    return new ExternalLoginTokenResult { Success = false };

                email = payload.Email;
                name = payload.Name;
                picture = payload.Picture;
            }
            else if (request.Provider.ToLower() == "facebook")
            {
                var validationUrl = $"https://graph.facebook.com/me?fields=id,name,email,picture&access_token={request.Token}";
                var response = await client.GetAsync(validationUrl);
                if (!response.IsSuccessStatusCode) return new ExternalLoginTokenResult { Success = false };

                var content = await response.Content.ReadAsStringAsync();
                var fbPayload = JsonSerializer.Deserialize<FacebookTokenPayload>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (fbPayload == null || string.IsNullOrEmpty(fbPayload.Email))
                    return new ExternalLoginTokenResult { Success = false };

                email = fbPayload.Email;
                name = fbPayload.Name;
                picture = fbPayload.Picture?.Data?.Url;
            }
            else
            {
                return new ExternalLoginTokenResult { Success = false };
            }

            // Verifica se o usuário já existe
            var user = await _userManager.FindByEmailAsync(email!);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    FullName = name,
                    EmailConfirmed = true
                };

                var createResult = await _userManager.CreateAsync(user);
                if (!createResult.Succeeded)
                    return new ExternalLoginTokenResult { Success = false };
            }
                       
            var jwt = _jwtTokenService.GenerateToken(user);

            var roles = await _userManager.GetRolesAsync(user);

            return new ExternalLoginTokenResult
            {
                Success = true,
                Token = jwt,
                User = new
                {
                    Name = user.FullName,
                    Email = user.Email,
                    Picture = picture,
                    Role = roles.FirstOrDefault() ?? ""
                }
            };
        }      
       

    }
}

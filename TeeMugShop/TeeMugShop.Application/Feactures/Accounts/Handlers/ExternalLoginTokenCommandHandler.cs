using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using TeeMugShop.Application.Common.Settings;
using TeeMugShop.Application.DTOs.Auth;
using TeeMugShop.Domain.Entities.Application;
using Microsoft.IdentityModel.Tokens;
using TeeMugShop.Application.Feactures.Accounts.Commands;

namespace TeeMugShop.Application.Features.Accounts.Commands
{
    public class ExternalLoginTokenCommandHandler : IRequestHandler<ExternalLoginTokenCommand, ExternalLoginTokenResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtSettings _jwtSettings;

        public ExternalLoginTokenCommandHandler(UserManager<ApplicationUser> userManager, IOptions<JwtSettings> jwtOptions)
        {
            _userManager = userManager;
            _jwtSettings = jwtOptions.Value;
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

            var jwt = GenerateJwtToken(user);

            return new ExternalLoginTokenResult
            {
                Success = true,
                Token = jwt,
                User = new
                {
                    Name = user.FullName,
                    Email = user.Email,
                    Picture = picture
                }
            };
        }        

        private string GenerateJwtToken(ApplicationUser user)
        {
            if (string.IsNullOrWhiteSpace(_jwtSettings.SecretKey) || _jwtSettings.SecretKey.Length < 16)
                throw new InvalidOperationException("JWT SecretKey is missing or too short (min 16 characters).");

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
                new Claim("name", user.FullName ?? "")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(_jwtSettings.TokenExpirationHours),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}

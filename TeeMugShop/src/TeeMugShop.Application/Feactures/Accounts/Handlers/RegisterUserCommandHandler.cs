using MediatR;
using Microsoft.AspNetCore.Identity;
using TeeMugShop.Application.Features.Accounts.Commands;
using TeeMugShop.Domain.Entities.Application;

namespace TeeMugShop.Application.Features.Accounts.Handlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, bool>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RegisterUserCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {            
            
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return false; 
            }

            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                FullName = request.FullName,
                Address = request.Address,
                NIF = request.NIF,
                PhoneNumber = request.Phone,
                EmailConfirmed = true 
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            return result.Succeeded;
        }
    }
}

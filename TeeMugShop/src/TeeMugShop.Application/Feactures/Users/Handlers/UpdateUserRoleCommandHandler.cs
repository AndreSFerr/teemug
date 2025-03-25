using MediatR;
using Microsoft.AspNetCore.Identity;
using TeeMugShop.Application.Feactures.Users.Commands;
using TeeMugShop.Domain.Entities.Application;

namespace TeeMugShop.Application.Feactures.Users.Handlers
{
    public class UpdateUserRoleCommandHandler : IRequest<UpdateUserRoleCommand>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UpdateUserRoleCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            var currentRoles = await _userManager.GetRolesAsync(user!);
            await _userManager.RemoveFromRolesAsync(user!, currentRoles);
            await _userManager.AddToRoleAsync(user!, request.NewRole);

            return Unit.Value;
        }
    }
}

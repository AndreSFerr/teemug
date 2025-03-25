using MediatR;
using Microsoft.AspNetCore.Identity;
using TeeMugShop.Application.Feactures.Users.Commands;
using TeeMugShop.Domain.Entities.Application;

public class BlockUserCommandHandler : IRequest<BlockUserCommand>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public BlockUserCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Unit> Handle(BlockUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user == null) throw new Exception("Usuário não encontrado.");

        user.LockoutEnabled = true;
        user.LockoutEnd = DateTime.MaxValue;
        await _userManager.UpdateAsync(user);

        return Unit.Value;
    }
}


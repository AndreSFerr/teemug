using MediatR;
using Microsoft.AspNetCore.Identity;
using TeeMugShop.Domain.Entities.Application;
using TeeMugShop.Application.DTOs;

public record GetAllUsersQuery : IRequest<List<UserDto>>;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserDto>>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public GetAllUsersQueryHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = _userManager.Users.Select(u => new UserDto
        {
            Id = u.Id,
            FullName = u.FullName!,
            Email = u.Email!
        }).ToList();

        return Task.FromResult(users);
    }
}

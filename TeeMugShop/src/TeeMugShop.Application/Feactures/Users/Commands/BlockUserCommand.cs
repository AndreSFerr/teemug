using MediatR;
using System;

namespace TeeMugShop.Application.Feactures.Users.Commands
{
    public class BlockUserCommand : IRequest
    {
        public Guid UserId { get; set; }
    }
}

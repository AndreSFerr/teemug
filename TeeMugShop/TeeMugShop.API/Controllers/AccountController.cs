using MediatR;
using Microsoft.AspNetCore.Mvc;
using TeeMugShop.Application.Features.Accounts.Commands;

namespace TeeMugShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            var success = await _mediator.Send(command);
            if (success)
                return Ok("Usuário registrado com sucesso.");
            else
                return BadRequest("Erro ao registrar o usuário.");
        }        
    }
}

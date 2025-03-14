using MediatR;
using Microsoft.AspNetCore.Mvc;
using TeeMugShop.Application.Carts.Commands;
using TeeMugShop.Application.Carts.Queries;

namespace TeeMugShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var carts = await _mediator.Send(new GetAllCartsQuery());
            return Ok(carts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var cart = await _mediator.Send(new GetCartByIdQuery { CartId = id });
            if (cart == null)
                return NotFound();

            return Ok(cart);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCartCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCartCommand command)
        {
            if (id != command.CartId)
                return BadRequest("CartId in URL does not match the request body.");

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteCartCommand { CartId = id });
            return NoContent();
        }
    }
}

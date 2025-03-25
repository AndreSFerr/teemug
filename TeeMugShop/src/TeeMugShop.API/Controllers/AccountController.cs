using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TeeMugShop.Application.Features.Accounts.Commands;
using TeeMugShop.Domain.Entities.Application;
using TeeMugShop.Application.Feactures.Accounts.Commands;

namespace TeeMugShop.API.Controllers
{
    /// <summary>
    /// Controller responsible for managing user accounts (registration, social login, etc.).
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
      
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="mediator">The MediatR instance for sending commands.</param>
        /// <param name="signInManager">ASP.NET Identity external sign-in manager.</param>
        /// <param name="userManager">ASP.NET Identity user manager.</param>      
        public AccountController(
            IMediator mediator,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _mediator = mediator;
            _signInManager = signInManager;
            _userManager = userManager;            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.Success)
            {
                return Ok(result);
            }

            return Unauthorized(result);
        }


        /// <summary>
        /// Registers a new user in the system.
        /// </summary>
        /// <param name="command">User registration data.</param>
        /// <returns>The result of the registration operation.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _mediator.Send(command);
            return success
                ? Ok(new { message = "User successfully registered." })
                : BadRequest(new { message = "Error registering user." });
        }
        

        /// <summary>
        /// Performs login using OAuth token received from the frontend (Google/Facebook).
        /// </summary>
        /// <param name="command">Token data received from the frontend.</param>
        /// <returns>The result of the authentication process.</returns>
        [HttpPost("external-login-token")]
        public async Task<IActionResult> ExternalLoginToken([FromBody] ExternalLoginTokenCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
      
    }
}

using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TeeMugShop.Application.Features.Accounts.Commands;
using TeeMugShop.Domain.Entities.Application;
using System.Security.Claims;
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
        /// Starts external login using the specified provider (Google or Facebook).
        /// </summary>
        /// <param name="provider">The name of the external authentication provider.</param>
        /// <param name="returnUrl">The URL to redirect to after login.</param>
        /// <returns>Redirection to the external authentication provider.</returns>
        [HttpGet("external-login")]
        public IActionResult ExternalLogin(string provider, string returnUrl = "/")
        {
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl })!;
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        /// <summary>
        /// Callback executed after external login is completed by the provider.
        /// </summary>
        /// <param name="returnUrl">The URL to redirect to after successful login.</param>
        /// <returns>Redirects the user or creates a new account if necessary.</returns>
        [HttpGet("external-login-callback")]
        public async Task<IActionResult> ExternalLoginCallback(string? returnUrl = "/")
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
                return Redirect("/login-failed");

            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
            if (result.Succeeded)
            {
                return Redirect(returnUrl ?? "/");
            }

            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            var name = info.Principal.FindFirstValue(ClaimTypes.Name);

            if (string.IsNullOrEmpty(email))
                return BadRequest(new { message = "Email not found from social login provider." });

            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                FullName = name ?? email
            };

            var createResult = await _userManager.CreateAsync(user);
            if (!createResult.Succeeded)
                return Problem("Failed to create user. The email might already be in use.");

            await _userManager.AddLoginAsync(user, info);
            await _signInManager.SignInAsync(user, isPersistent: false);

            return Redirect(returnUrl ?? "/");
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
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}

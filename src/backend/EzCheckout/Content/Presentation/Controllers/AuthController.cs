namespace EzCheckout.Presentation.Controllers;

using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using EzCheckout.Content.Core.Models.Identity;
using EzCheckout.Content.Diagnostics;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

/// <summary>
/// Handles authentication and user management for the application.
/// </summary>
[ApiController]
[Route("api/auth")]
public partial class AuthController : ControllerBase {

    // ---------- Private readonly fields ----------

    /// <summary>
    /// Stores the logger for this instance.
    /// </summary>
    private readonly ILogger<AuthController> _logger;

    /// <summary>
    /// Stores the sign-in manager for this instance.
    /// </summary>
    private readonly SignInManager<ApplicationUser> _signInManager;

    /// <summary>
    /// Stores the user manager for this instance.
    /// </summary>
    private readonly UserManager<ApplicationUser> _userManager;


    // ---------- Public constructors ----------

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthController"/> class.
    /// </summary>
    /// <param name="userManager">The user manager.</param>
    /// <param name="signInManager">The sign-in manager.</param>
    /// <param name="logger">The logger.</param>
    public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AuthController> logger)
        : base() {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
    }


    // ---------- Public methods ----------

    /// <summary>
    /// Logs in a user.
    /// </summary>
    /// <param name="model">The login information.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginModel model) {
        using Activity? activity = Diagnostics.ActivitySource.StartActivity(
                name: "AuthController.Login",
                kind: ActivityKind.Server);
        activity?.AddTag("username", model.Username);

        using (_logger.BeginScope("Login [{Username}]", model.Username)) {
            Log.LogLoginAttempt(_logger, model.Username);

            SignInResult result = await _signInManager.PasswordSignInAsync(
                    model.Username,
                    model.Password,
                    model.RememberMe,
                    lockoutOnFailure: false)
                .ConfigureAwait(continueOnCapturedContext: false);

            if (result.Succeeded) {
                Log.LogLoginSuccess(_logger, model.Username);
                return Ok(new { success = true, message = "Login successful" });
            }

            Log.LogLoginFailed(_logger, model.Username);
            return Unauthorized(new { success = false, message = "Invalid login attempt" });
        }
    }

    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="model">The registration information.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterModel model) {
        using Activity? activity = Diagnostics.ActivitySource.StartActivity(
                name: "AuthController.Register",
                kind: ActivityKind.Server);
        activity?.AddTag("username", model.Username);

        using (_logger.BeginScope("Register [{Username}]", model.Username)) {
            Log.LogRegisterAttempt(_logger, model.Username);

            ApplicationUser user = new (){
                UserName = model.Username
            };

            IdentityResult result = await _userManager.CreateAsync(user, model.Password)
                .ConfigureAwait(continueOnCapturedContext: false);

            if (result.Succeeded) {
                Log.LogRegisterSuccess(_logger, model.Username);
                await _signInManager.SignInAsync(user, isPersistent: false)
                    .ConfigureAwait(continueOnCapturedContext: false);
                return Ok(new { success = true, message = "Registration successful" });
            }

            Log.LogRegisterFailed(_logger, model.Username, result.Errors.First().Description);
            return BadRequest(new { success = false, errors = result.Errors.Select(e => e.Description) });
        }
    }

    /// <summary>
    /// Logs out the current user.
    /// </summary>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout() {
        string username = User.Identity?.Name ?? "unknown";
        using Activity? activity = Diagnostics.ActivitySource.StartActivity(
                name: "AuthController.Logout",
                kind: ActivityKind.Server);
        activity?.AddTag("username", username);

        using (_logger.BeginScope("Logout [{Username}]", username)) {
            await _signInManager.SignOutAsync()
                .ConfigureAwait(continueOnCapturedContext: false);
            Log.LogLogout(_logger, username);
            return Ok(new { success = true, message = "Logout successful" });
        }
    }

    /// <summary>
    /// Gets information about the current user.
    /// </summary>
    /// <returns>Information about the current user.</returns>
    [HttpGet("user")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUser() {
        string? username = User.Identity?.Name;
        if (username == null) {
            return NotFound();
        }

        ApplicationUser? user = await _userManager.FindByNameAsync(username)
            .ConfigureAwait(continueOnCapturedContext: false);
        if (user == null) {
            return NotFound();
        }

        return Ok(new {
            username = user.UserName,
            email = user.Email,
            firstName = user.FirstName,
            lastName = user.LastName
        });
    }
}
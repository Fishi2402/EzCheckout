namespace EzCheckout.Api.Auth;

using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;


/// <summary>
/// Provides a mock authentication handler for testing authentication flows in ASP.NET Core applications.
/// </summary>
/// <remarks>This handler issues a fixed set of claims for a mock user and is intended for use in development or
/// testing environments where real authentication is not required. It should not be used in production scenarios, as it
/// does not perform any credential validation.</remarks>
public class MockAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions> {

    /// <summary>
    /// Initializes a new instance of the MockAuthHandler class with the specified authentication options, logger
    /// factory, and URL encoder.
    /// </summary>
    /// <param name="options">The monitor that provides the authentication scheme options used to configure the handler.</param>
    /// <param name="logger">The factory used to create logger instances for logging within the handler.</param>
    /// <param name="encoder">The encoder used to encode URLs as part of the authentication process.</param>
    public MockAuthHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder)
        : base(options, logger, encoder) {
    }

    /// <summary>
    /// Handles the authentication process for the current request using a mock identity.
    /// </summary>
    /// <remarks>This method provides a mock authentication result intended for development or testing
    /// scenarios. It always authenticates as a fixed local developer user and should not be used in production
    /// environments.</remarks>
    /// <returns>A <see cref="Task{TResult}"/> that represents the asynchronous authentication operation.
    /// The task result contains an <see cref="AuthenticateResult"/> indicating a successful authentication
    /// with a mock user identity.</returns>
    protected override Task<AuthenticateResult> HandleAuthenticateAsync(){
        Claim[] claims = [
            new Claim(ClaimTypes.NameIdentifier, "local-user"),
            new Claim(ClaimTypes.Name, "Local developer"),
            new Claim("role", "developer")
        ];

        ClaimsIdentity identiy = new (claims, "Mock");
        ClaimsPrincipal principal = new(identiy);
        AuthenticationTicket ticket = new(principal, "Mock");

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}

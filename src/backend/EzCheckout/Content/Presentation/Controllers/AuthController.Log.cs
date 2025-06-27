namespace EzCheckout.Presentation.Controllers;

using Microsoft.Extensions.Logging;

public partial class AuthController {
    private static partial class Log {
        // ---------- Login ----------

        /// <summary>
        /// Logs "Login attempt for user [{username}].".
        /// </summary>
        [LoggerMessage(
            eventId: 1,
            level: LogLevel.Information,
            message: "Login attempt for user [{username}].")]
        internal static partial void LogLoginAttempt(ILogger logger, string username);

        /// <summary>
        /// Logs "Login successful for user [{username}].".
        /// </summary>
        [LoggerMessage(
            eventId: 2,
            level: LogLevel.Information,
            message: "Login successful for user [{username}].")]
        internal static partial void LogLoginSuccess(ILogger logger, string username);

        /// <summary>
        /// Logs "Login failed for user [{username}].".
        /// </summary>
        [LoggerMessage(
            eventId: 3,
            level: LogLevel.Warning,
            message: "Login failed for user [{username}].")]
        internal static partial void LogLoginFailed(ILogger logger, string username);

        // ---------- Registration ----------

        /// <summary>
        /// Logs "Registration attempt for user [{username}].".
        /// </summary>
        [LoggerMessage(
            eventId: 4,
            level: LogLevel.Information,
            message: "Registration attempt for user [{username}].")]
        internal static partial void LogRegisterAttempt(ILogger logger, string username);

        /// <summary>
        /// Logs "Registration successful for user [{username}].".
        /// </summary>
        [LoggerMessage(
            eventId: 5,
            level: LogLevel.Information,
            message: "Registration successful for user [{username}].")]
        internal static partial void LogRegisterSuccess(ILogger logger, string username);

        /// <summary>
        /// Logs "Registration failed for user [{username}]: [{error}].".
        /// </summary>
        [LoggerMessage(
            eventId: 6,
            level: LogLevel.Warning,
            message: "Registration failed for user [{username}]: [{error}].")]
        internal static partial void LogRegisterFailed(ILogger logger, string username, string error);

        // ---------- Logout ----------

        /// <summary>
        /// Logs "Logout for user [{username}].".
        /// </summary>
        [LoggerMessage(
            eventId: 7,
            level: LogLevel.Information,
            message: "Logout for user [{username}].")]
        internal static partial void LogLogout(ILogger logger, string username);
    }
}
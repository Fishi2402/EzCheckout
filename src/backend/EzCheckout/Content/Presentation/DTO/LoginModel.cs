namespace EzCheckout.Presentation;

using System;

/// <summary>
/// User login model.
/// </summary>
/// <param name="Username">A <see cref="String"/> with the name of the user.</param>
/// <param name="Password"> A <see cref="String"/> with the password of the user.</param>
/// <param name="RememberMe"> A <see cref="Boolean"/> indicating whether the user should be remembered.</param>
public record LoginModel(
        string Username,
        string Password,
        bool RememberMe
    );
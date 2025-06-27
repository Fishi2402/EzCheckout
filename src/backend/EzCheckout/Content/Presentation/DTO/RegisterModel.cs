namespace EzCheckout.Presentation;

using System;

/// <summary>
/// User login model.
/// </summary>
/// <param name="Username">A <see cref="String"/> with the name of the user.</param>
/// <param name="Password"> A <see cref="String"/> with the password of the user.</param>
public record RegisterModel(
        string Username,
        string Password,
        bool RememberMe
    );
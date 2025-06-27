namespace EzCheckout.Content.Core.Models.Identity;

using Microsoft.AspNetCore.Identity;

/// <summary>
/// Represents a user in the EzCheckout application.
/// </summary>
public class ApplicationUser : IdentityUser{
    // ---------- Public constructors ----------

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationUser"/> class.
    /// </summary>
    public ApplicationUser()
            : base() {
        FirstName = string.Empty;
        LastName = string.Empty;
    }


    // ---------- Public properties ----------

    /// <summary>
    /// Gets or sets the user's first name.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the user's last name.
    /// </summary>
    public string LastName { get; set; }
}
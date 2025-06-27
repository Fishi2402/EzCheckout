namespace EzCheckout.Data.Entities;


using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


using System.ComponentModel.DataAnnotations;

/// <summary>
/// Represents a checkout entity for persistence.
/// </summary>
[Table("checkouts")]
public class CheckoutEntity {
    // ---------- Public properties ----------

    /// <summary>
    /// Gets or sets the unique identifier for the checkout.
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Identifier { get; set; }

    /// <summary>
    /// Gets or sets the name of the checkout station.
    /// </summary>
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = default!;

    /// <summary>
    /// Navigation property for items available at this checkout.
    /// </summary>
    public ICollection<ItemEntity> AvailableItems { get; set; } = [];
}


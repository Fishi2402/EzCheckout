namespace EzCheckout.Data.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Represents an item entity for persistence.
/// </summary>
[Table("item")]
public class ItemEntity {
    // ---------- Public properties ----------

    /// <summary>
    /// Gets or sets the unique identifier for the item.
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Identifier { get; set; }

    /// <summary>
    /// Gets or sets the name of the item.
    /// </summary>
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = default!;

    /// <summary>
    /// Gets or sets the description for the item.
    /// </summary>
    [MaxLength(1000)]
    public string Description { get; set; } = default!;

    /// <summary>
    /// Gets or sets the price of the item in cents.
    /// </summary>
    [Required]
    public int Price { get; set; }
}
namespace EzCheckout.Data.Entities;

using System.ComponentModel.DataAnnotations.Schema;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// Represents a junction entity linking orders and items, including quantity.
/// </summary>
[Table("order_items")]
public class OrderItemEntity {
    // ---------- Public properties ----------

    /// <summary>
    /// Gets or sets the identifier for the associated order.
    /// </summary>
    [Key, Column(Order = 0)]
    public int OrderId { get; set; }

    /// <summary>
    /// Gets or sets the identifier for the associated item.
    /// </summary>
    [Key, Column(Order = 1)]
    public int ItemId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the item in the order.
    /// </summary>
    [Required]
    public int Quantity { get; set; }

    /// <summary>
    /// Navigation property for the associated order.
    /// </summary>
    public OrderEntity Order { get; set; } = default!;

    /// <summary>
    /// Navigation property for the associated item.
    /// </summary>
    public ItemEntity Item { get; set; } = default!;
}
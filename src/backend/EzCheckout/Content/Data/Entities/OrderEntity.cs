namespace EzCheckout.Data.Entities;

using EzCheckout.Core.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using System;

/// <summary>
/// Represents an order entity for persistence.
/// </summary>
[Table("orders")]
public class OrderEntity {
    // ---------- Public properties ----------

    /// <summary>
    /// Gets or sets the unique identifier for the order.
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Identifier { get; set; }

    /// <summary>
    /// Gets or sets the type of the order.
    /// </summary>
    [Required]
    [Column("type")]
    public OrderType Type { get; set; }

    /// <summary>
    /// Gets or sets the creation time of the order.
    /// </summary>
    [Required]
    [Column("created", TypeName = "timestamp with time zone")]
    public DateTime Created { get; set; }

    /// <summary>
    /// Gets or sets the completion time of the order, if completed.
    /// </summary>
    [Column("completed", TypeName = "timestamp with time zone")]
    public DateTime? Completed { get; set; }

    /// <summary>
    /// Navigation property for associated order items.
    /// </summary>
    public ICollection<OrderItemEntity> OrderItems { get; set; } = [];
}
namespace EzCheckout.Core.Models;

using System;

/// <summary>
/// Represents an item that can be sold on a checkout.
/// </summary>
public class Item {
    // ---------- Public constructors ----------

    /// <summary>
    /// Initializes a new instance of the <see cref="Item"/> class.
    /// </summary>
    /// <param name="identifier">The unique identifier for the item.</param>
    /// <param name="description">The description for the item.</param>
    /// <param name="name">The name of the item.</param>
    /// <param name="price">The price for the item in cents</param>
    public Item(
            int identifier,
            string description,
            string name,
            int price)
        : base()
    {
        Identifier = identifier;
        Description = description;
        Name = name;
        Price = price;
    }


    // ---------- Public properties ----------

    /// <summary>
    /// Gets the description for the item.
    /// </summary>
    /// <value>A <see cref="String"/ with the description for the item.></value>
    public string Description { get; }

    /// <summary>
    /// Gets the unique identifier for the item.
    /// </summary>
    /// <value>A <see cref="Int32"/> with the unique identifier for the item.
    /// </value>
    public int Identifier { get; }

    /// <summary>
    /// Gets the name for the item.
    /// </summary>
    /// <value>A <see cref="String"/> with the name for the item.</value>
    public string Name { get; }

    /// <summary>
    /// Gets the price for the item.
    /// </summary>
    /// <value>A <see cref="Int32"/> representing the price for the item in cents.</value>
    public int Price { get; }
}

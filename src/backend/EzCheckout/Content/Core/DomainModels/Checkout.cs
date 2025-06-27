namespace EzCheckout.Core.Models;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

/// <summary>
/// Represents a checkout that can be used to sell items.
/// </summary>
public class Checkout {
    // ---------- Public constructors ----------

    /// <summary>
    /// Initializes a new instance of the <see cref="Checkout"/> class.
    /// </summary>
    /// <param name="identifier">The unique identifier for the checkout station.</param>
    /// <param name="name">The name for the checkout station.</param>
    public Checkout(
            int identifier,
            string name)
        : base()
    {
        Identifier = identifier;
        Name = name;
        AvailableItems = [];
    }


    // ---------- Public properties ----------

    /// <summary>
    /// Gets the items that can be sold at the checkout station.
    /// </summary>
    /// <value>An <see cref="ICollection{T}"/> with <see cref="Item"/>-instances
    /// representing the items that can be sold at the checkout station.</value>
    public Collection<Item> AvailableItems {
        get;
        private init;
    }

    /// <summary>
    /// Gets the unique identifier for the checkout station.
    /// </summary>
    /// <value>A <see cref="Int32"/> with the unique identifier for the checkout
    /// station.</value>
    public int Identifier { get; }

    /// <summary>
    /// Gets the name for the checkout station.
    /// </summary>
    /// <value>A <see cref="String"/> with the name for the checkout station.</value>
    public string Name { get; }
}

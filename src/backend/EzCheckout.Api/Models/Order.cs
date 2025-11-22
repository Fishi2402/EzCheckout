namespace EzCheckout.Api.Models;

using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Represents an order in the system.
/// </summary>
public class Order {

    /// <summary>
    /// Initializes a new instance of the <see cref="Order"/> class.
    /// </summary>
    /// <param name="identifier">The unique identifier of the order.</param>
    /// <param name="type">The type of the order.</param>
    /// <param name="created">The creation time of the order.</param>
    public Order(
            int identifier,
            OrderType type,
            DateTime created)
        : base() {
        Identifier = identifier;
        Type = type;
        Created = created;
        Completed = null;
        Items = [];
    }

    /// <summary>
    /// Initializes a new instance of a completed <see cref="Order"/>.
    /// </summary>
    /// <param name="identifier">The unique identifier of the order.</param>
    /// <param name="created">The creation time of the order.</param>
    /// <param name="completed">The completion time of the order.</param>
    /// <param name="items">The items with their quantity of the order.</param>
    /// <param name="type">The type of the order.</param>
    public Order(
            int identifier,
            DateTime created,
            DateTime? completed,
            Dictionary<Item, int> items,
            OrderType type) {
        Completed = completed;
        Created = created;
        Identifier = identifier;
        Items = items;
        Type = type;
    }


    /// <summary>
    /// Gets the time when the order was completed, if it has been completed.
    /// </summary>
    /// <value>A <see cref="DateTime"/> with the completion time, if it has
    /// been completed; otherwise <see langword="null"/></value>
    public DateTime? Completed { get; private set; }

    /// <summary>
    /// Gets the time when the order was created.
    /// </summary>
    /// <value>A <see cref="DateTime"/> with the creation time.</value>
    public DateTime Created { get; }

    /// <summary>
    /// Gets the unique identifier for the order.
    /// </summary>
    /// <value>A <see cref="Int32"/> with the unique identifier for the order.</value>
    public int Identifier { get; }

    /// <summary>
    /// Gets a value indicating whether the order is completed.
    /// </summary>
    /// <value>A <see cref="Boolean"/> indication if the order is completed.</value>
    public bool IsCompleted => Completed.HasValue;

    /// <summary>
    /// Gets the items with their quantity for the order.
    /// </summary>
    /// <value>A <see cref="Dictionary{TKey, TValue}"/> containing the items of the
    /// order with the respective quantity. </value>
    public Dictionary<Item, int> Items {
        get;
        private init;
    }

    /// <summary>
    /// Gets the total price of the order.
    /// </summary>
    /// <value>An <see cref="Int32"/> with the total price of the order in cents.</value>
    public int TotalPrice => Items.Sum(item => item.Key.Price * item.Value);

    /// <summary>
    /// Gets the type of the order.
    /// </summary>
    /// <value>A <see cref="OrderType"/> with the type of the order.</value>
    public OrderType Type { get; }


    /// <summary>
    /// Completes the order.
    /// </summary>
    /// <exception cref="InvalidOperationException">The order is already completed.</exception>
    public void SetCompleted() {
        if (IsCompleted) {
            throw new InvalidOperationException("The order is already completed.");
        }
        Completed = DateTime.UtcNow;
    }

}

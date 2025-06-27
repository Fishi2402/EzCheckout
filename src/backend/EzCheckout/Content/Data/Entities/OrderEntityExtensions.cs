namespace EzCheckout.Data.Entities;

using System.Collections.Generic;
using System.Linq;

using EzCheckout.Core.Models;

/// <summary>
/// Contains extension methods for the <see cref="OrderEntity"/> class.
/// </summary>
public static class OrderEntityExtensions {
    // ---------- Public static methods ----------

    /// <summary>
    /// Converts a <see cref="OrderEntity"/> to a <see cref="Order"/>.
    /// </summary>
    /// <param name="entity">The entity to convert.</param>
    /// <returns>The corresponding <see cref="Order"/> instance.</returns>
    public static Order ToOrder(this OrderEntity entity) {
        Order order = new(
            identifier: entity.Identifier,
            type: entity.Type,
            created: entity.Created,
            completed: entity.Completed,
            items: entity.OrderItems.Select(orderItemEntity =>
                new KeyValuePair<Item, int>(
                    key: orderItemEntity.Item.ToItem(),
                    value: orderItemEntity.Quantity)).ToDictionary()
        );
        return order;
    }

    /// <summary>
    /// Converts a <see cref="Order"/> to a <see cref="OrderEntity"/>.
    /// </summary>
    /// <param name="Item">The instance to convert.</param>
    /// <returns>The corresponding <see cref="OrderEntity"/> instance.</returns>
    public static OrderEntity ToEntity(this Order Item)
        => new() {
            Identifier = Item.Identifier,
            Type = Item.Type,
            Created = Item.Created,
            Completed = Item.Completed,
            OrderItems = [.. Item.Items.Select(item =>
                new OrderItemEntity {
                    Item = item.Key.ToEntity(),
                    Quantity = item.Value
                })]
        };
}

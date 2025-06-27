namespace EzCheckout.Data.Entities;

using EzCheckout.Core.Models;

/// <summary>
/// Contains extension methods for the <see cref="ItemEntity"/> class.
/// </summary>
public static class ItemEntityExtensions {
    // ---------- Public static methods ----------

    /// <summary>
    /// Converts a <see cref="ItemEntity"/> to a <see cref="Item"/>.
    /// </summary>
    /// <param name="entity">The entity to convert.</param>
    /// <returns>The corresponding <see cref="Item"/> instance.</returns>
    public static Item ToItem(this ItemEntity entity)
        => new(
            identifier: entity.Identifier,
            description: entity.Description,
            name: entity.Name,
            price: entity.Price);

    /// <summary>
    /// Converts a <see cref="Item"/> to a <see cref="ItemEntity"/>.
    /// </summary>
    /// <param name="Item">The instance to convert.</param>
    /// <returns>The corresponding <see cref="ItemEntity"/> instance.</returns>
    public static ItemEntity ToEntity(this Item Item)
        => new() {
            Identifier = Item.Identifier,
            Description = Item.Description,
            Name = Item.Name,
            Price = Item.Price
        };
}

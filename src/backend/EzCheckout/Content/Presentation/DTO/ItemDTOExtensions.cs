namespace EzCheckout.Presentation;

using EzCheckout.Core.Models;

public static class ItemDTOExtensions {
    // ---------- Public static methods ----------

    /// <summary>
    /// Converts a <see cref="ItemDTO"/> to a <see cref="Item"/>.
    /// </summary>
    /// <param name="entity">The entity to convert.</param>
    /// <returns>The corresponding <see cref="Item"/> instance.</returns>
    public static Item ToItem(this ItemDTO entity)
        => new(
            identifier: entity.Identifier,
            description: entity.Description,
            name: entity.Name,
            price: entity.Price);

    /// <summary>
    /// Converts a <see cref="Item"/> to a <see cref="ItemDTO"/>.
    /// </summary>
    /// <param name="Item">The instance to convert.</param>
    /// <returns>The corresponding <see cref="ItemDTO"/> instance.</returns>
    public static ItemDTO ToDTO(this Item Item)
        => new(
            Identifier: Item.Identifier,
            Description: Item.Description,
            Name: Item.Name,
            Price: Item.Price
        );
}

namespace EzCheckout.Api.DTO.Extensions;

using EzCheckout.Api.DomainModels;
using EzCheckout.Api.DTO;

/// <summary>
/// Contains extension methods for converting between <see cref="Item"/> domain
/// instances and their corresponding <see cref="ItemDTO"/> representations.
/// </summary>
public static class ItemDTOExtensions
{
    /// <summary>
    /// Converts an <see cref="Item"/> domain instance to an <see cref="ItemDTO"/>.
    /// </summary>
    /// <param name="item">The domain <see cref="Item"/> to convert.</param>
    /// <returns>An <see cref="ItemDTO"/> containing the identifier, name, description
    /// and price taken from the specified <see cref="Item"/>.</returns>
    public static ItemDTO ToDto(this Item item)
        => new (item.Identifier, item.Name, item.Price, item.Description);

    /// <summary>
    /// Converts an <see cref="ItemDTO"/> to the corresponding domain <see cref="Item"/>.
    /// </summary>
    /// <param name="dto">The <see cref="ItemDTO"/> to convert.</param>
    /// <returns>An <see cref="Item"/> created from the data contained in the specified
    /// <see cref="ItemDTO"/>.</returns>
    public static Item ToDomain(this ItemDTO dto)
        => new (dto.Identifier, dto.Description, dto.Name, dto.Price);
}

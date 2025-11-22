namespace EzCheckout.Api.DTO.Extensions;

using System.Linq;
using EzCheckout.Api.DomainModels;
using EzCheckout.Api.DTO;

/// <summary>
/// Contains extension methods for converting between <see cref="Checkout"/> and the corresponding
/// <see cref="CheckoutDTO"/> instances..
/// </summary>
public static class CheckoutDTOExtensions
{
    /// <summary>
    /// Converts a <see cref="Checkout"/> instance to its corresponding <see cref="CheckoutDTO"/> representation.
    /// </summary>
    /// <param name="checkout">The Checkout object to convert.</param>
    /// <returns>A <see cref="CheckoutDTO"/>> object containing the identifier, name, and available
    /// items from the specified <see cref="Checkout"/>>.</returns>
    public static CheckoutDTO ToDto(this Checkout checkout)
    {
        ItemDTO[] items = checkout.AvailableItems.Select(i => i.ToDto()).ToArray();
        return new CheckoutDTO(checkout.Identifier, checkout.Name, items);
    }

    /// <summary>
    /// Converts a <see cref="CheckoutDTO"/> instance to its corresponding <see cref="Checkout"/> model.
    /// </summary>
    /// <param name="dto">The <see cref="CheckoutDTO"/> object to convert.</param>
    /// <returns>A <see cref="Checkout"/> domain model populated with data from the specified
    /// <see cref="CheckoutDTO"/>.</returns>
    public static Checkout ToDomain(this CheckoutDTO dto)
    {
        Checkout checkout = new Checkout(dto.Identifier, dto.Name);
        foreach (ItemDTO itemDto in dto.AvailableItems)
        {
            checkout.AvailableItems.Add(itemDto.ToDomain());
        }
        return checkout;
    }
}

namespace EzCheckout.Api.DTO.Extensions;

using System;
using System.Linq;
using System.Collections.Generic;
using EzCheckout.Api.DomainModels;
using EzCheckout.Api.DTO;

/// <summary>
/// Contains extension methods for converting between <see cref="Order"/> domain
/// instances and their corresponding <see cref="OrderDTO"/> representations.
/// </summary>
public static class OrderDTOExtensions
{
    /// <summary>
    /// Converts an <see cref="Order"/> domain instance to an <see cref="OrderDTO"/>.
    /// </summary>
    /// <param name="order">The <see cref="Order"/> instance to convert.</param>
    /// <returns>An <see cref="OrderDTO"/> that contains identifier, type, timestamps,
    /// items and total price from the specified <see cref="Order"/>.</returns>
    public static OrderDTO ToDto(this Order order)
    {
        OrderItemDTO[] items = [.. order.Items.Select(i => i.ToDto())];
        return new OrderDTO(order.Identifier, order.Type.ToString(), order.Created, order.Completed, items, order.TotalPrice);
    }

    /// <summary>
    /// Converts an <see cref="OrderDTO"/> to the corresponding domain <see cref="Order"/>.
    /// </summary>
    /// <param name="dto">The <see cref="OrderDTO"/> to convert.</param>
    /// <returns>An <see cref="Order"/> populated with the data from the specified
    /// <see cref="OrderDTO"/>.</returns>
    public static Order ToDomain(this OrderDTO dto)
    {
        Dictionary<Item, int> items = dto.Items.ToDictionary(i => i.Item.ToDomain(), i => i.Quantity);
        OrderType orderType = Enum.Parse<OrderType>(dto.Type);
        return new Order(dto.Identifier, dto.Created, dto.Completed, items, orderType);
    }


    /// <summary>
    /// Converts a Key/Value pair representing an <see cref="Item"/> and its
    /// quantity to an <see cref="OrderItemDTO"/> representation.
    /// </summary>
    /// <param name="kvp">A <see cref="KeyValuePair{TKey,TValue}"/> where the key
    /// is the <see cref="Item"/> and the value is the quantity.</param>
    /// <returns>An <see cref="OrderItemDTO"/> containing the item DTO and quantity.</returns>
    private static OrderItemDTO ToDto(this KeyValuePair<Item, int> kvp)
        => new(kvp.Key.ToDto(), kvp.Value);
}

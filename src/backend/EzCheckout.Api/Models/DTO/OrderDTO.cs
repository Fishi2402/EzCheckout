namespace EzCheckout.Api.DTO;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Data Transfer Object for an order.
/// </summary>
/// <param name="Identifier">Unique identifier of the order.</param>
/// <param name="Type">Type of the order.</param>
/// <param name="Created">Creation time of the order (UTC).</param>
/// <param name="Completed">Completion time of the order (UTC) or null.</param>
/// <param name="Items">Items with quantities in the order.</param>
/// <param name="TotalPrice">Total price of the order in cents.</param>
public record OrderDTO(
    [property:JsonPropertyName("identifier"), JsonPropertyOrder(1)]
        int Identifier,
    [property:JsonPropertyName("type"), JsonPropertyOrder(2)]
        string Type,
    [property:JsonPropertyName("created"), JsonPropertyOrder(3)]
        DateTime Created,
    [property:JsonPropertyName("completed"), JsonPropertyOrder(4)]
        DateTime? Completed,
    [property:JsonPropertyName("items"), JsonPropertyOrder(5)]
        IReadOnlyCollection<OrderItemDTO> Items,
    [property:JsonPropertyName("totalPrice"), JsonPropertyOrder(6)]
        int TotalPrice);

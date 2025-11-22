namespace EzCheckout.Api.DTO;

using System.Text.Json.Serialization;

/// <summary>
/// Represents an item entry inside an order with quantity information.
/// </summary>
/// <param name="Item">The item details.</param>
/// <param name="Quantity">The quantity of the item in the order.</param>
public record OrderItemDTO(
    [property:JsonPropertyName("item"), JsonPropertyOrder(1)]
        ItemDTO Item,
    [property:JsonPropertyName("quantity"), JsonPropertyOrder(2)]
        int Quantity);

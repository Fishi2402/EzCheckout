namespace EzCheckout.Api.DTO;

using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Data Transfer Object for a checkout station.
/// </summary>
/// <param name="Identifier">Unique identifier of the checkout.</param>
/// <param name="Name">Name of the checkout.</param>
/// <param name="AvailableItems">Items available at the checkout.</param>
public record CheckoutDTO(
    [property:JsonPropertyName("identifier"), JsonPropertyOrder(1)]
        int Identifier,
    [property:JsonPropertyName("name"), JsonPropertyOrder(2)]
        string Name,
    [property:JsonPropertyName("availableItems"), JsonPropertyOrder(3)]
        IReadOnlyCollection<ItemDTO> AvailableItems);

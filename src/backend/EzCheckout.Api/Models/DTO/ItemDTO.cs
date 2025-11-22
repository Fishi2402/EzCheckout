namespace EzCheckout.Api.DTO;

using System.Text.Json.Serialization;

/// <summary>
/// Represents a data transfer object for an item in the EzCheckout application.
/// </summary>
/// <param name="Identifier">The identifier of the item.</param>
/// <param name="Name">The name of the item.</param>
/// <param name="Price">The price of the item in cents.</param>
/// <param name="Description">The description of the item.</param>
public record ItemDTO(
    [property:JsonPropertyName("identifier"), JsonPropertyOrder(1)]
        int Identifier,
    [property:JsonPropertyName("name"), JsonPropertyOrder(2)]
        string Name,
    [property:JsonPropertyName("price"), JsonPropertyOrder(3)]
        int Price,
    [property:JsonPropertyName("description"), JsonPropertyOrder(4)]
        string Description);

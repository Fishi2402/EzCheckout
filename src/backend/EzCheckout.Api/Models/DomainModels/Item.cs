namespace EzCheckout.Api.DomainModels;

/// <summary>
/// Represents an item that can be sold on a checkout.
/// </summary>
/// <param name="Identifier">The unique identifier for the item.</param>
/// <param name="Name">The name of the item.</param>
/// <param name="Description">A brief description of the item.</param>
/// <param name="Price">The price of the item in cents.</param>
public readonly record struct Item(
    int Identifier,
    string Description,
    string Name,
    int Price);
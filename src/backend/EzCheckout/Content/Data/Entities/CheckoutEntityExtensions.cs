namespace EzCheckout.Data.Entities;

using System.Linq;

using EzCheckout.Core.Models;

/// <summary>
/// Contains extension methods for the <see cref="CheckoutEntity"/> class.
/// </summary>
public static class CheckoutEntityExtensions {
    // ---------- Public static methods ----------

    /// <summary>
    /// Converts a <see cref="CheckoutEntity"/> to a <see cref="Checkout"/>.
    /// </summary>
    /// <param name="entity">The entity to convert.</param>
    /// <returns>The corresponding <see cref="Checkout"/> instance.</returns>
    public static Checkout ToCheckout(this CheckoutEntity entity) {
        Checkout checkout = new(
            identifier: entity.Identifier,
            name: entity.Name);

        entity.AvailableItems.ToList().ForEach(itemEntity => {
            checkout.AvailableItems.Add(itemEntity.ToItem());
        });

        return checkout;
    }

    /// <summary>
    /// Converts a <see cref="Checkout"/> to a <see cref="CheckoutEntity"/>.
    /// </summary>
    /// <param name="checkout">The instance to convert.</param>
    /// <returns>The corresponding <see cref="CheckoutEntity"/> instance.</returns>
    public static CheckoutEntity ToEntity(this Checkout checkout) {
        CheckoutEntity entity = new() {
            Identifier = checkout.Identifier,
            Name = checkout.Name
        };
        checkout.AvailableItems.ToList().ForEach(item => {
            entity.AvailableItems.Add(item.ToEntity());
        });
        return entity;
    }
}

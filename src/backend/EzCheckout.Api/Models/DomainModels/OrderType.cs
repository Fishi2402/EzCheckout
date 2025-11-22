namespace EzCheckout.Api.DomainModels;
/// <summary>
/// Defines constants that represent the different types of an <see cref="Order"/>.
/// </summary>
public enum OrderType {
    /// <summary>
    /// Represents a default order for a customer.
    /// </summary>
    Customer = 0,
    /// <summary>
    /// Represents an order for an employee.
    /// </summary>
    Employee = 1,
}
﻿namespace EzCheckout.Core.Models;

/// <summary>
/// Defines constants that represent the different types for 
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
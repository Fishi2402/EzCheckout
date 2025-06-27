namespace EzCheckout.Data;

using Microsoft.EntityFrameworkCore;
using EzCheckout.Data.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using EzCheckout.Content.Core.Models.Identity;

/// <summary>
/// Represents the database context for items in the EzCheckout application.
/// </summary>
public partial class EzCheckoutContext : IdentityDbContext<ApplicationUser> {
    // ---------- Private fields ----------

    /// <summary>
    /// Stores the logger for this instance.
    /// </summary>
    private readonly ILogger<EzCheckoutContext> _logger;

    // ---------- Public constructors ----------

    /// <summary>
    /// Initializes a new instance of the <see cref="EzCheckoutContext"/> class using the specified options.
    /// </summary>
    /// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
    /// <param name="logger">The logger to use for this context.</param>
    public EzCheckoutContext(
        DbContextOptions<EzCheckoutContext> options,
        ILogger<EzCheckoutContext> logger)
        : base(options) {
        _logger = logger;
    }


    // ---------- Protected methods ----------

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        // Call the base implementation first - this is critical for Identity tables
        base.OnModelCreating(modelBuilder);

        // Configure item-table
        _ = modelBuilder.Entity<ItemEntity>();

        // Configure OrderItemEntity composite key
        _ = modelBuilder.Entity<OrderItemEntity>()
            .HasKey(oe => new { oe.OrderId, oe.ItemId });

        // Store enum as int explicitly
        _ = modelBuilder.Entity<OrderEntity>()
            .Property(e => e.Type)
            .HasConversion<int>();

        // Configure relationship between OrderEntity and OrderItemEntity
        _ = modelBuilder.Entity<OrderItemEntity>()
            .HasOne(oe => oe.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oe => oe.OrderId);
    }
}


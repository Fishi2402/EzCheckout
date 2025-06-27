namespace EzCheckout.Data;

using Microsoft.EntityFrameworkCore;
using EzCheckout.Data.Entities;

/// <summary>
/// Represents the database context for items in the EzCheckout application.
/// </summary>
public partial class EzCheckoutContext : DbContext {
    // ---------- Public constructors ----------

    /// <summary>
    /// Initializes a new instance of the <see cref="EzCheckoutContext"/> class using the specified options.
    /// </summary>
    /// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
    public EzCheckoutContext(DbContextOptions<EzCheckoutContext> options) 
        : base(options)
    {
    }


    // ---------- Protected methods ----------

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
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


namespace EzCheckout.Data;

using EzCheckout.Core.Models;
using EzCheckout.Data.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

public partial class EzCheckoutContext {
    // ---------- Private properties ----------

    /// <summary>
    /// Gets or sets the items in the database.
    /// </summary>
    private DbSet<ItemEntity> Items { get; set; }


    // ---------- Public methods ----------

    /// <summary>
    /// Creates a new item in the database.
    /// </summary>
    /// <param name="item">The item to create.</param>
    /// <returns>The added item.</returns>
    public async Task<Item> CreateItemAsync(Item item) {
        ItemEntity entity = item.ToEntity();
        EntityEntry entityEntry = Items.Add(entity);
        Debug.Assert(entityEntry.State == EntityState.Added);
        int changes = await SaveChangesAsync().ConfigureAwait(continueOnCapturedContext: false);
        Debug.Assert(changes == 1);

        return entity.ToItem();
    }

    /// <summary>
    /// Updates an item in the database.
    /// </summary>
    /// <param name="item">The item to update.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. The result
    /// parameter contains a <see cref="Item"/> that is the updated item..</returns>
    public async Task<Item> UpdateItemAsync(Item item) {
        ItemEntity entity = item.ToEntity();
        EntityEntry entityEntry = Items.Update(entity);
        Debug.Assert(entityEntry.State == EntityState.Modified);
        int changes = await SaveChangesAsync().ConfigureAwait(continueOnCapturedContext: false);
        Debug.Assert(changes == 1);
        return entity.ToItem();
    }

    /// <summary>
    /// Gets an item from the database.
    /// </summary>
    /// <param name="identifier">The identifier of the item.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. The result
    /// parameter contains a <see cref="Item"/> if there was match; otherwise <see langword="null"/>.
    /// </returns>
    public async Task<Item?> GetItemAsync(int identifier) {
        ItemEntity? entity = await Items.FindAsync(identifier)
            .ConfigureAwait(continueOnCapturedContext: false);
        return entity?.ToItem();
    }

    /// <summary>
    /// Gets all items from the database.
    /// </summary>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. The result
    /// parameter contains a <see cref="Collection{T}"/> with the <see cref="Item"/> instances.
    /// </returns>
    public async Task<Collection<Item>> GetItemsAsync() {
        Collection<Item> items = [];
        await foreach (ItemEntity item in Items.AsAsyncEnumerable()
                .ConfigureAwait(continueOnCapturedContext: false)) {
            items.Add(item.ToItem());
        }
        return items;
    }


    /// <summary>
    /// Deletes an item from the database.
    /// </summary>
    /// <param name="identifier">The identifier of the item to delete.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. The result
    /// parameter contains a <see cref="Item"/> if an item was deleted; otherwise <see langword="false"/>.
    /// </returns>
    public async Task<Item?> DeleteItemAsync(int identifier) {
        ItemEntity? entity = await Items.FindAsync(identifier)
            .ConfigureAwait(continueOnCapturedContext: false);
        if (entity == null) {
            return null;
        } else {

            EntityEntry<ItemEntity> entityEntry = Items.Remove(entity);
            Debug.Assert(entityEntry.State == EntityState.Deleted);
            int changes = await SaveChangesAsync()
                .ConfigureAwait(continueOnCapturedContext: false);
            Debug.Assert(changes == 1);
            return entity.ToItem();
        }
    }
}

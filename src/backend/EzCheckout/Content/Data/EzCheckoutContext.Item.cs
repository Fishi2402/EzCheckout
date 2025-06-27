namespace EzCheckout.Data;

using EzCheckout.Core.Models;
using EzCheckout.Data.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using EzCheckout.Content.Diagnostics;
using Microsoft.Extensions.Logging;

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
        using Activity? activity = Diagnostics.ActivitySource.StartActivity(
            name: "EzCheckoutContext.CreateItemAsync",
            kind: ActivityKind.Internal);
        activity?
            .AddTag("item.id", item.Identifier)
            .AddTag("item.name", item.Name)
            .AddTag("item.price", item.Price);

        using (_logger.BeginScope("Item [{ItemId}]", item.Identifier)) {
            Log.LogCreateItem(_logger, item.Identifier, item.Name, item.Price);

            ItemEntity entity = item.ToEntity();
            EntityEntry entityEntry = Items.Add(entity);
            Debug.Assert(entityEntry.State == EntityState.Added);

            int changes = await SaveChangesAsync().ConfigureAwait(continueOnCapturedContext: false);
            Log.LogSavedChanges(_logger, changes);

            if (changes != 1) {
                Log.LogUnexpectedChangeCount(_logger, changes);
            }

            Log.LogItemCreated(_logger, entity.Identifier);
            return entity.ToItem();
        }
    }

    /// <summary>
    /// Updates an item in the database.
    /// </summary>
    /// <param name="item">The item to update.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. The result
    /// parameter contains a <see cref="Item"/> that is the updated item..</returns>
    public async Task<Item> UpdateItemAsync(Item item) {
        using Activity? activity = Diagnostics.ActivitySource.StartActivity(
            name: "EzCheckoutContext.UpdateItemAsync",
            kind: ActivityKind.Internal);
        activity?
            .AddTag("item.id", item.Identifier)
            .AddTag("item.name", item.Name)
            .AddTag("item.price", item.Price);

        using (_logger.BeginScope("Item [{ItemId}]", item.Identifier)) {
            Log.LogUpdateItem(_logger, item.Identifier, item.Name, item.Price);

            ItemEntity entity = item.ToEntity();
            EntityEntry entityEntry = Items.Update(entity);
            Debug.Assert(entityEntry.State == EntityState.Modified);

            int changes = await SaveChangesAsync().ConfigureAwait(continueOnCapturedContext: false);
            Log.LogSavedChanges(_logger, changes);

            if (changes != 1) {
                Log.LogUnexpectedChangeCount(_logger, changes);
            }

            Log.LogItemUpdated(_logger, item.Identifier);
            return entity.ToItem();
        }
    }

    /// <summary>
    /// Gets an item from the database.
    /// </summary>
    /// <param name="identifier">The identifier of the item.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. The result
    /// parameter contains a <see cref="Item"/> if there was match; otherwise <see langword="null"/>.
    /// </returns>
    public async Task<Item?> GetItemAsync(int identifier) {
        using Activity? activity = Diagnostics.ActivitySource.StartActivity(
            name: "EzCheckoutContext.GetItemAsync",
            kind: ActivityKind.Internal);
        activity?.AddTag("item.id", identifier);

        using (_logger.BeginScope("Item [{ItemId}]", identifier)) {
            Log.LogGetItem(_logger, identifier);

            ItemEntity? entity = await Items.FindAsync(identifier)
                .ConfigureAwait(continueOnCapturedContext: false);

            if (entity == null) {
                Log.LogItemNotFound(_logger, identifier);
                return null;
            }

            Log.LogItemRetrieved(_logger, entity.Identifier, entity.Name);
            return entity.ToItem();
        }
    }

    /// <summary>
    /// Gets all items from the database.
    /// </summary>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. The result
    /// parameter contains a <see cref="Collection{T}"/> with the <see cref="Item"/> instances.
    /// </returns>
    public async Task<Collection<Item>> GetItemsAsync() {
        using Activity? activity = Diagnostics.ActivitySource.StartActivity(
            name: "EzCheckoutContext.GetItemsAsync",
            kind: ActivityKind.Internal);

        using (_logger.BeginScope("GetAllItems")) {
            Log.LogGetItems(_logger);

            Collection<Item> items = [];
            await foreach (ItemEntity item in Items.AsAsyncEnumerable()
                    .ConfigureAwait(continueOnCapturedContext: false)) {
                items.Add(item.ToItem());
            }

            Log.LogItemsRetrieved(_logger, items.Count);
            return items;
        }
    }

    /// <summary>
    /// Deletes an item from the database.
    /// </summary>
    /// <param name="identifier">The identifier of the item to delete.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. The result
    /// parameter contains a <see cref="Item"/> if an item was deleted; otherwise <see langword="false"/>.
    /// </returns>
    public async Task<Item?> DeleteItemAsync(int identifier) {
        using Activity? activity = Diagnostics.ActivitySource.StartActivity(
            name: "EzCheckoutContext.DeleteItemAsync",
            kind: ActivityKind.Internal);
        activity?.AddTag("item.id", identifier);

        using (_logger.BeginScope("Item [{ItemId}]", identifier)) {
            Log.LogDeleteItem(_logger, identifier);

            ItemEntity? entity = await Items.FindAsync(identifier)
                .ConfigureAwait(continueOnCapturedContext: false);

            if (entity == null) {
                Log.LogItemNotFound(_logger, identifier);
                return null;
            }

            Item deletedItem = entity.ToItem();
            EntityEntry<ItemEntity> entityEntry = Items.Remove(entity);
            Debug.Assert(entityEntry.State == EntityState.Deleted);

            int changes = await SaveChangesAsync()
                .ConfigureAwait(continueOnCapturedContext: false);
            Log.LogSavedChanges(_logger, changes);

            if (changes != 1) {
                Log.LogUnexpectedChangeCount(_logger, changes);
            }

            Log.LogItemDeleted(_logger, identifier);
            return deletedItem;
        }
    }
}

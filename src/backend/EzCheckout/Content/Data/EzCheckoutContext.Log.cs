namespace EzCheckout.Data;

using Microsoft.Extensions.Logging;

public partial class EzCheckoutContext {
    /// <summary>
    /// Provides all logging functionality for the <see cref="EzCheckoutContext"/> class.
    /// </summary>
    private static partial class Log {

        // ---------- Create item ----------

        /// <summary>
        /// Logs "Creating item with id [{itemId}], name '[{itemName}]' and price [{price}].".
        /// </summary>
        [LoggerMessage(
            eventId: 1,
            level: LogLevel.Information,
            message: "Creating item with id [{itemId}], name '[{itemName}]' and price [{price}].")]
        internal static partial void LogCreateItem(ILogger logger, int itemId, string itemName, int price);

        /// <summary>
        /// Logs "Successfully created item with id [{itemId}].".
        /// </summary>
        [LoggerMessage(
            eventId: 2,
            level: LogLevel.Information,
            message: "Successfully created item with id [{itemId}].")]
        internal static partial void LogItemCreated(ILogger logger, int itemId);


        // ---------- Read item ----------

        /// <summary>
        /// Logs "Getting item with id [{itemId}].".
        /// </summary>
        [LoggerMessage(
            eventId: 3,
            level: LogLevel.Debug,
            message: "Getting item with id [{itemId}].")]
        internal static partial void LogGetItem(ILogger logger, int itemId);

        /// <summary>
        /// Logs "Retrieved item [{itemId}]: '[{itemName}]'.".
        /// </summary>
        [LoggerMessage(
            eventId: 4,
            level: LogLevel.Debug,
            message: "Retrieved item [{itemId}]: '[{itemName}]'.")]
        internal static partial void LogItemRetrieved(ILogger logger, int itemId, string itemName);

        /// <summary>
        /// Logs "Item with id [{itemId}] not found.".
        /// </summary>
        [LoggerMessage(
            eventId: 5,
            level: LogLevel.Debug,
            message: "Item with id [{itemId}] not found.")]
        internal static partial void LogItemNotFound(ILogger logger, int itemId);

        /// <summary>
        /// Logs "Getting all items.".
        /// </summary>
        [LoggerMessage(
            eventId: 6,
            level: LogLevel.Debug,
            message: "Getting all items.")]
        internal static partial void LogGetItems(ILogger logger);

        /// <summary>
        /// Logs "Retrieved [{count}] items.".
        /// </summary>
        [LoggerMessage(
            eventId: 7,
            level: LogLevel.Debug,
            message: "Retrieved [{count}] items.")]
        internal static partial void LogItemsRetrieved(ILogger logger, int count);


        // ---------- Update item ----------

        /// <summary>
        /// Logs "Updating item with id [{itemId}], name '[{itemName}]' and price [{price}].".
        /// </summary>
        [LoggerMessage(
            eventId: 8,
            level: LogLevel.Information,
            message: "Updating item with id [{itemId}], name '[{itemName}]' and price [{price}].")]
        internal static partial void LogUpdateItem(ILogger logger, int itemId, string itemName, int price);

        /// <summary>
        /// Logs "Successfully updated item with id [{itemId}].".
        /// </summary>
        [LoggerMessage(
            eventId: 9,
            level: LogLevel.Information,
            message: "Successfully updated item with id [{itemId}].")]
        internal static partial void LogItemUpdated(ILogger logger, int itemId);


        // ---------- Delete item ----------

        /// <summary>
        /// Logs "Deleting item with id [{itemId}].".
        /// </summary>
        [LoggerMessage(
            eventId: 10,
            level: LogLevel.Information,
            message: "Deleting item with id [{itemId}].")]
        internal static partial void LogDeleteItem(ILogger logger, int itemId);

        /// <summary>
        /// Logs "Successfully deleted item with id [{itemId}].".
        /// </summary>
        [LoggerMessage(
            eventId: 11,
            level: LogLevel.Information,
            message: "Successfully deleted item with id [{itemId}].")]
        internal static partial void LogItemDeleted(ILogger logger, int itemId);


        // ---------- Database operations ----------

        /// <summary>
        /// Logs "Saved [{changes}] changes to database.".
        /// </summary>
        [LoggerMessage(
            eventId: 12,
            level: LogLevel.Debug,
            message: "Saved [{changes}] changes to database.")]
        internal static partial void LogSavedChanges(ILogger logger, int changes);

        /// <summary>
        /// Logs "Expected to affect 1 row but affected [{changes}] rows.".
        /// </summary>
        [LoggerMessage(
            eventId: 13,
            level: LogLevel.Warning,
            message: "Expected to affect 1 row but affected [{changes}] rows.")]
        internal static partial void LogUnexpectedChangeCount(ILogger logger, int changes);
    }
}

namespace EzCheckout.Presentation;

using Microsoft.Extensions.Logging;

public partial class ItemsController {

    /// <summary>
    /// Provides all logging functionality for the <see cref="ItemsController"/> class.
    /// </summary>
    private static partial class Log {

        // ---------- Create item ----------

        /// <summary>
        /// Logs "Creating item with price [{price}].".
        /// </summary>
        [LoggerMessage(
            eventId: 1,
            level: LogLevel.Information,
            message: "Creating item with price [{price}].")]
        internal static partial void LogCreateItem(ILogger logger, int price);


        // ---------- Get items ----------

        /// <summary>
        /// Logs "Getting item with id {itemId}.".
        /// </summary>
        [LoggerMessage(
            eventId: 2,
            level: LogLevel.Information,
            message: "Getting item with id {itemId}.")]
        internal static partial void LogGetItem(ILogger logger, int itemId);

        /// <summary>
        /// Logs "Retrieved item {itemId}: '{itemName}'.".
        /// </summary>
        [LoggerMessage(
            eventId: 3,
            level: LogLevel.Information,
            message: "Retrieved item {itemId}: '{itemName}'.")]
        internal static partial void LogItemRetrieved(ILogger logger, int itemId, string itemName);

        /// <summary>
        /// Logs "Getting all available items.".
        /// </summary>
        [LoggerMessage(
            eventId: 4,
            level: LogLevel.Information,
            message: "Getting all available items.")]
        internal static partial void LogGetItems(ILogger logger);

        /// <summary>
        /// Logs "Retrieved {count} items.".
        /// </summary>
        [LoggerMessage(
            eventId: 5,
            level: LogLevel.Information,
            message: "Retrieved {count} items.")]
        internal static partial void LogItemsRetrieved(ILogger logger, int count);


        // ---------- Update item ----------

        /// <summary>
        /// Logs "Updating item {itemId}: '{itemName}' with price {price}.".
        /// </summary>
        [LoggerMessage(
            eventId: 6,
            level: LogLevel.Information,
            message: "Updating item {itemId}: '{itemName}' with price {price}.")]
        internal static partial void LogUpdateItem(ILogger logger, int itemId, string itemName, int price);

        /// <summary>
        /// Logs "Item {itemId}: '{itemName}' successfully updated.".
        /// </summary>
        [LoggerMessage(
            eventId: 7,
            level: LogLevel.Information,
            message: "Item {itemId}: '{itemName}' successfully updated.")]
        internal static partial void LogItemUpdated(ILogger logger, int itemId, string itemName);

        /// <summary>
        /// Logs "Identifier mismatch in update request. URL id: {urlId}, item id: {itemId}.".
        /// </summary>
        [LoggerMessage(
            eventId: 8,
            level: LogLevel.Warning,
            message: "Identifier mismatch in update request. URL id: {urlId}, item id: {itemId}.")]
        internal static partial void LogUpdateItemIdentifierMismatch(ILogger logger, int urlId, int itemId);


        // ---------- Delete item ----------

        /// <summary>
        /// Logs "Deleting item with id {itemId}.".
        /// </summary>
        [LoggerMessage(
            eventId: 9,
            level: LogLevel.Information,
            message: "Deleting item with id {itemId}.")]
        internal static partial void LogDeleteItem(ILogger logger, int itemId);

        /// <summary>
        /// Logs "Item {itemId}: '{itemName}' successfully deleted.".
        /// </summary>
        [LoggerMessage(
            eventId: 10,
            level: LogLevel.Information,
            message: "Item {itemId}: '{itemName}' successfully deleted.")]
        internal static partial void LogItemDeleted(ILogger logger, int itemId, string itemName);


        // ---------- Common messages ----------

        /// <summary>
        /// Logs "Item with id {itemId} not found.".
        /// </summary>
        [LoggerMessage(
            eventId: 11,
            level: LogLevel.Warning,
            message: "Item with id {itemId} not found.")]
        internal static partial void LogItemNotFound(ILogger logger, int itemId);
    }
}


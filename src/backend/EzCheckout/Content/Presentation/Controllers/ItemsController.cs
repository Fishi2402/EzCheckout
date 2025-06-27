namespace EzCheckout.Presentation;

using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using EzCheckout.Content.Diagnostics;
using EzCheckout.Core.Models;
using EzCheckout.Data;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

/// <summary>
/// Represents the controller for items in the EzCheckout application.
/// </summary>
[Route("api/items")]
[ApiController]
public partial class ItemsController : ControllerBase {

    // ---------- Private fields ----------

    /// <summary>
    /// Stores the database context for this instance.
    /// </summary>
    private readonly EzCheckoutContext _context;


    /// <summary>
    /// Stores the logger for this instance.
    /// </summary>
    private readonly ILogger<ItemsController> _logger;


    // ---------- Public constructors ----------

    /// <summary>
    /// Initializes a new instance of the <see cref="ItemsController"/> class.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/> to use for the instance.</param>
    /// <param name="context">The <see cref="EzCheckoutContext"/> to use as database context.</param>
    public ItemsController(
            ILogger<ItemsController> logger,
            EzCheckoutContext context) {
        _logger = logger;
        _context = context;
    }


    // ---------- Public methods ----------

    /// <summary>
    /// Creates a new item.
    /// </summary>
    /// <param name="item"></param>
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<ItemDTO>> CreateItem(ItemDTO item) {
        using Activity? activity = Diagnostics.ActivitySource.StartActivity(
                name: "ItemsController.CreateItem",
                kind: ActivityKind.Server);
        activity?
            .AddTag("item.id", item.Identifier)
            .AddTag("item.name", item.Name);

        using (_logger.BeginScope("Item [{ItemId} ({ItemName})]", item.Identifier, item.Name)) {
            Log.LogCreateItem(_logger, item.Price);

            Item createdItem = await _context.CreateItemAsync(item.ToItem());
            return CreatedAtAction(
                actionName: nameof(CreateItem),
                routeValues: "test/TODO",
                value: createdItem.ToDTO());
        }
    }

    /// <summary>
    /// Deletes an existing item.
    /// </summary>
    /// <param name="id">The identifier of the item to delete.</param>
    [HttpDelete]
    [Route("{id}")]
    [Authorize]
    public async Task<ActionResult> DeleteItem(int id) {
        using Activity? activity = Diagnostics.ActivitySource.StartActivity(
                name: "ItemsController.DeleteItem",
                kind: ActivityKind.Server);
        activity?.AddTag("id", id);

        using (_logger.BeginScope("Item [{ItemId}]", id)) {
            Log.LogDeleteItem(_logger, id);

            Item? deletedItem = await _context.DeleteItemAsync(id);
            if (deletedItem == null) {
                Log.LogItemNotFound(_logger, id);
                return NotFound();
            }

            Log.LogItemDeleted(_logger, id, deletedItem.Name);
            return NoContent();
        }
    }

    /// <summary>
    /// Gets an item by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the item.</param>
    [HttpGet]
    [Route("{id}")]
    [Authorize]
    public async Task<ActionResult<ItemDTO>> GetItem(int id) {
        using Activity? activity = Diagnostics.ActivitySource.StartActivity(
                name: "ItemsController.GetItem",
                kind: ActivityKind.Server);
        activity?.AddTag("id", id);

        using (_logger.BeginScope("Item [{ItemId}]", id)) {
            Log.LogGetItem(_logger, id);

            Item? item = await _context.GetItemAsync(id);
            if (item == null) {
                Log.LogItemNotFound(_logger, id);
                return NotFound();
            }

            Log.LogItemRetrieved(_logger, id, item.Name);
            return item.ToDTO();
        }
    }

    /// <summary>
    /// Gets all available items.
    /// </summary>
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<ItemDTO[]>> GetItems() {
        using Activity? activity = Diagnostics.ActivitySource.StartActivity(
                name: "ItemsController.GetItems",
                kind: ActivityKind.Server);

        using (_logger.BeginScope("GetAllItems")) {
            Log.LogGetItems(_logger);

            Collection<Item> items = await _context.GetItemsAsync()
                .ConfigureAwait(continueOnCapturedContext: false);

            Log.LogItemsRetrieved(_logger, items.Count);
            return items.Select(item => item.ToDTO()).ToArray();
        }
    }

    /// <summary>
    /// Updates an existing item.
    /// </summary>
    /// <param name="id">The identifier of the item to update.</param>
    /// <param name="item">The updated item data.</param>
    [HttpPut]
    [Route("{id}")]
    [Authorize]
    public async Task<ActionResult<ItemDTO>> UpdateItem(int id, ItemDTO item) {
        using Activity? activity = Diagnostics.ActivitySource.StartActivity(
                name: "ItemsController.UpdateItem",
                kind: ActivityKind.Server);
        activity?.AddTag("id", id)
            .AddTag("item.id", item.Identifier)
            .AddTag("item.name", item.Name);

        using (_logger.BeginScope("Item [{ItemId} ({ItemName})]", id, item.Name)) {
            Log.LogUpdateItem(_logger, id, item.Name, item.Price);

            if (id != item.Identifier) {
                Log.LogUpdateItemIdentifierMismatch(_logger, id, item.Identifier);
                return BadRequest("Identifier mismatch.");
            }

            Item? existingItem = await _context.GetItemAsync(id);
            if (existingItem == null) {
                Log.LogItemNotFound(_logger, id);
                return NotFound();
            }

            Item updatedItem = await _context.UpdateItemAsync(item.ToItem());
            Log.LogItemUpdated(_logger, updatedItem.Identifier, updatedItem.Name);
            return Ok(updatedItem.ToDTO());
        }
    }
}

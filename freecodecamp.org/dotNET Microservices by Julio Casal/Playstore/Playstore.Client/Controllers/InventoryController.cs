using MassTransit.Initializers;
using Microsoft.AspNetCore.Mvc;
using Playstore.Client.Models.Catalog;
using Playstore.Client.Models.Inventory;
using Playstore.Client.ServiceClients;

namespace Playstore.Client.Controllers;

/// <summary>Operations with inventory items.</summary>
public class InventoryController : Controller
{
    /// <summary>Client for communicating with the catalog service.</summary>
    private readonly ICatalogClient _catalogClient;

    /// <summary>Client for communicating with the inventory service.</summary>
    private readonly IInventoryClient _inventoryClient;

    /// <summary>ID of the user owning the inventory.</summary>
    /// <remarks>Hardcoded until user/identity service available.</remarks>
    private  Guid _hardcodedUserId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6");

    public InventoryController(IInventoryClient inventoryClient, ICatalogClient catalogClient)
    {
        _inventoryClient = inventoryClient;
        _catalogClient = catalogClient;
    }

    /// <summary>Lists the items in the inventory.</summary>
    /// <returns>Inventory/Index page.</returns>
    public async Task<IActionResult> Index()
    {
        IEnumerable<InventoryItemViewModel> items = (await _inventoryClient.GetItemsAsync(_hardcodedUserId))
            .Select(dto =>  new InventoryItemViewModel
            {
                Id = dto.CatalogItemId,
                Name = dto.Name,
                Description = dto.Description,
                Quantity = dto.Quantity
            });

        return View(items);
    }

    /// <summary>Lists catalog items for purchase.</summary>
    /// <returns>Inventory/Purchase page.</returns>
    public async Task<IActionResult> Purchase()
    {
        IEnumerable<CatalogItemViewModel> catalogItems = (await _catalogClient.GetAllItemsAsync())
            .Select(dto => new CatalogItemViewModel
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price
            });

        return View(catalogItems);
    }
}
using System.Net;
using MassTransit.Initializers;
using Microsoft.AspNetCore.Mvc;
using Playstore.Catalog.Contracts.DataTransferObjects;
using Playstore.Client.Models;
using Playstore.Client.Models.Catalog;
using Playstore.Client.Models.Inventory;
using Playstore.Client.ServiceClients;
using Playstore.Inventory.Contracts.DataTransferObjects;

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
    [HttpGet]
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
    [HttpGet]
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

    /// <summary>Page for purchasing an item.</summary>
    /// <param name="id">ID of the item in the catalog.</param>
    /// <returns>Inventory/PurchaseItem page.</returns>
    [HttpGet]
    public async Task<IActionResult> PurchaseItem(Guid? id)
    {
        if (!id.HasValue)
        {
            return NotFound();
        }

        try
        {
            CatalogItemDto item = await _catalogClient.GetItem(id.Value);
            return View(new PurchaseItemViewModel
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Price = item.Price
            });
        }
        catch (HttpRequestException exception)
        {
            if (exception.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound();
            }

            TempData[NotificationsViewModel.c_Error] = "An error has occured on the server";
            return RedirectToAction("Index");
        }
    }

    /// <summary>Purchase an item.</summary>
    /// <param name="item">The item to purchase.</param>
    /// <returns>Redirects to Inventory/Index page.</returns>
    [HttpPost]
    public async Task<IActionResult> PurchaseItem(PurchaseItemViewModel item)
    {
        if (ModelState.IsValid)
        {
            await _inventoryClient.GrantInventoryItems(new GrantInventoryItemsDto(_hardcodedUserId, item.Id, item.Quantity));
            TempData[NotificationsViewModel.c_Success] = "Items purchased successfully";
            return RedirectToAction("Index");
        }
        else
        {
            return View(item);
        }
    }
}
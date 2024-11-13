using System.IdentityModel.Tokens.Jwt;
using System.Net;
using MassTransit.Initializers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Playstore.Catalog.Contracts.DataTransferObjects;
using Playstore.Client.Models;
using Playstore.Client.Models.Catalog;
using Playstore.Client.Models.Inventory;
using Playstore.Client.ServiceClients;
using Playstore.Inventory.Contracts.DataTransferObjects;

namespace Playstore.Client.Controllers;

/// <summary>Operations with inventory items.</summary>
[Authorize(Roles = "Player")]
public class InventoryController : Controller
{
    /// <summary>Client for communicating with the catalog service.</summary>
    private readonly ICatalogClient _catalogClient;

    /// <summary>Client for communicating with the inventory service.</summary>
    private readonly IInventoryClient _inventoryClient;

    public InventoryController(IInventoryClient inventoryClient, ICatalogClient catalogClient)
    {
        _inventoryClient = inventoryClient;
        _catalogClient = catalogClient;
    }

    /// <summary>Lists the items in the inventory.</summary>
    /// <returns>Inventory/Index page.</returns>
    [HttpGet]
    public async Task<IActionResult> IndexAsync()
    {
        IReadOnlyCollection<InventoryItemDto> inventoryItems = await _inventoryClient.GetItemsAsync(GetCurrentUserId());
        IEnumerable<InventoryItemViewModel> items = inventoryItems
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
    public async Task<IActionResult> PurchaseAsync()
    {
        IReadOnlyCollection<CatalogItemDto> catalogItems = await _catalogClient.GetAllItemsAsync();
        IEnumerable<CatalogItemViewModel> items = catalogItems
            .Select(dto => new CatalogItemViewModel
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price
            });

        return View(items);
    }

    /// <summary>Page for purchasing an item.</summary>
    /// <param name="id">ID of the item in the catalog.</param>
    /// <returns>Inventory/PurchaseItem page.</returns>
    [HttpGet]
    public async Task<IActionResult> PurchaseItemAsync(Guid? id)
    {
        if (!id.HasValue)
        {
            return NotFound();
        }

        try
        {
            CatalogItemDto item = await _catalogClient.GetItemAsync(id.Value);
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
    public async Task<IActionResult> PurchaseItemAsync(PurchaseItemViewModel item)
    {
        if (ModelState.IsValid)
        {
            await _inventoryClient.GrantInventoryItems(new GrantInventoryItemsDto(GetCurrentUserId(), item.Id, item.Quantity));
            TempData[NotificationsViewModel.c_Success] = "Items purchased successfully";
            return RedirectToAction("Index");
        }
        else
        {
            return View(item);
        }
    }

    private Guid GetCurrentUserId()
    {
        var userId = User.Claims.Single(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value;
        return new Guid(userId);
    }
}
using System.Net;
using MassTransit.Initializers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Playstore.Catalog.Contracts.DataTransferObjects;
using Playstore.Client.Models;
using Playstore.Client.Models.Catalog;
using Playstore.Client.ServiceClients;

namespace Playstore.Client.Controllers;

/// <summary>Operations on catalog items.</summary>
[Authorize(Roles = "GameMaster")]
public class CatalogController : Controller
{
    /// <summary>Client for communicating with the catalog service.</summary>
    private readonly ICatalogClient _catalogClient;

    private const string c_ServerError = "An error has occured on the server.";

    /// <summary>Create new instance.</summary>
    /// <param name="catalogClient">Client for communicating with the catalog service.</param>
    public CatalogController(ICatalogClient catalogClient)
    {
        _catalogClient = catalogClient;
    }

    /// <summary>Displays all catalog items.</summary>
    /// <returns>Catalog/Index page.</returns>
    [HttpGet]
    public async Task<IActionResult> IndexAsync()
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

    /// <summary>Page to create new item.</summary>
    /// <returns>Catalog/Create page.</returns>
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    /// <summary>Create a new item.</summary>
    /// <param name="item">The item to create.</param>
    /// <returns>Redirects to catalog index.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateAsync(CatalogItemViewModel item)
    {
        if (ModelState.IsValid)
        {
            await _catalogClient.CreateItemAsync(new CreateCatalogItemDto(item.Name, item.Description, item.Price));
            TempData[NotificationsViewModel.c_Success] = "Catalog item created successfully";

            return RedirectToAction("Index");
        }
        else
        {
            return View(item);
        }
    }

    /// <summary>Page to edit an item.</summary>
    /// <param name="id">ID of the item to edit.</param>
    /// <returns>Catalog/Edit page.</returns>
    [HttpGet]
    public async Task<IActionResult> EditAsync(Guid? id)
    {
        if (!id.HasValue)
        {
            return NotFound();
        }
        try
        {
            CatalogItemDto dto = await _catalogClient.GetItemAsync(id.Value);
            return View(new CatalogItemViewModel
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price
            });
        }
        catch (HttpRequestException exception)
        {
            if (exception.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound();
            }

            TempData[NotificationsViewModel.c_Error] = c_ServerError;
            return RedirectToAction("Index");
        }
    }

    /// <summary>Update an item in the catalog.</summary>
    /// <param name="item">The item to update.</param>
    /// <returns>Redirects to catalog index.</returns>
    [HttpPost]
    public async Task<IActionResult> EditAsync(CatalogItemViewModel item)
    {
        if (ModelState.IsValid)
        {
            await _catalogClient.UpdateItemAsync(item.Id, new UpdateCatalogItemDto(item.Name, item.Description, item.Price));

            TempData[NotificationsViewModel.c_Success] = "Catalog item updated successfully";
            return RedirectToAction("Index");
        }
        else
        {
            return View(item);
        }
    }

    /// <summary>Page to delete an item from the catalog.</summary>
    /// <param name="id">ID of the item to delete.</param>
    /// <returns>Catalog/Delete page.</returns>
    [HttpGet]
    public async Task<IActionResult> DeleteAsync(Guid? id)
    {
        if (!id.HasValue)
        {
            return NotFound();
        }
        try
        {
            CatalogItemDto dto = await _catalogClient.GetItemAsync(id.Value);
            return View(new CatalogItemViewModel
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price
            });
        }
        catch (HttpRequestException exception)
        {
            if (exception.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound();
            }

            TempData[NotificationsViewModel.c_Error] = c_ServerError;
            return RedirectToAction("Index");
        }
    }

    /// <summary>Delete an item from the catalog.</summary>
    /// <param name="id">ID of the item to delete.</param>
    /// <returns>Redirects to catalog index.</returns>
    [HttpPost]
    public async Task<IActionResult> DeleteConfirmAsync(Guid id)
    {
        await _catalogClient.DeleteItemAsync(id);

        TempData[NotificationsViewModel.c_Success] = "Catalog item deleted successfully";
        return RedirectToAction("Index");
    }
}
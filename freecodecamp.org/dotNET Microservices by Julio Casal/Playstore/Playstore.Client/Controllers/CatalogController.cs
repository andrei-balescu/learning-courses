using System.Net;
using MassTransit.Initializers;
using Microsoft.AspNetCore.Mvc;
using Playstore.Catalog.Contracts.DataTransferObjects;
using Playstore.Client.Models;
using Playstore.Client.ServiceClients;

namespace Playstore.Client.Controllers;

/// <summary>Operations on catalog items.</summary>
public class CatalogController : Controller
{
    /// <summary>Client for communicating with the catalog service.</summary>
    private readonly ICatalogClient _catalogClient;

    /// <summary>Create new instance.</summary>
    /// <param name="catalogClient">Client for communicating with the catalog service.</param>
    public CatalogController(ICatalogClient catalogClient)
    {
        _catalogClient = catalogClient;
    }

    /// <summary>Displays all catalog items.</summary>
    /// <returns>Catalog/Index page.</returns>
    [HttpGet]
    public async Task<IActionResult> Index()
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
    public async Task<IActionResult> Create()
    {
        return View();
    }

    /// <summary>Create a new item.</summary>
    /// <param name="item">The item to create.</param>
    /// <returns>Redirects to Catalog index or back to page if any validation errors.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CatalogItemViewModel item)
    {
        if (ModelState.IsValid)
        {
            await _catalogClient.CreateItem(new CreateCatalogItemDto(item.Name, item.Description, item.Price));

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
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (!id.HasValue)
        {
            return NotFound();
        }
        try
        {
            CatalogItemDto dto = await _catalogClient.GetItem(id.Value);
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

            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(CatalogItemViewModel item)
    {
        if (ModelState.IsValid)
        {
            await _catalogClient.UpdateItem(item.Id, new UpdateCatalogItemDto(item.Name, item.Description, item.Price));

            return RedirectToAction("Index");
        }
        else
        {
            return View(item);
        }
    }
}
using MassTransit.Initializers;
using Microsoft.AspNetCore.Mvc;
using Playstore.Client.Dtos;
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
}
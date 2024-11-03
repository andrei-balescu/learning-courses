using Microsoft.AspNetCore.Mvc;
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
        IReadOnlyCollection<Dto.CatalogItemDto> catalogItems = await _catalogClient.GetAllItemsAsync();

        return View(catalogItems);
    }
}
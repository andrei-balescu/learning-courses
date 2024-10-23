using BulkyBook.Data;
using BulkyBook.Data.Models;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Controllers;

/// <summary>Controller that manages categories.</summary>
public class CategoryController : Controller
{
    private readonly ApplicationDbContext _dbContext;

    public CategoryController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>Lists all catogories.</summary>
    /// <returns>Category/Index page</returns>
    [HttpGet]
    public IActionResult Index()
    {
        IEnumerable<CategoryViewModel> categories = _dbContext.Categories
            .Select(m => new CategoryViewModel { Name = m.Name, DisplayOrder = m.DisplayOrder })
            .OrderBy(c => c.DisplayOrder);

        return View(categories);
    }

    /// <summary>Page to reate a new category</summary>
    /// <returns>Category/Create page</returns>
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    /// <summary>Creates a new category</summary>
    /// <param name="category">The category to create.</param>
    /// <returns>Redirects to category index or back to category creation if any errors.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CategoryViewModel category)
    {
        if (string.Equals(category.Name, category.DisplayOrder.ToString(), StringComparison.CurrentCulture))
        {
            ModelState.AddModelError("Name", "The Display Order cannot exactly match the name");
        }

        if (_dbContext.Categories.Any(c => c.Name == category.Name))
        {
            ModelState.AddModelError("Name", "Name already exists");
        }

        if (_dbContext.Categories.Any(c => c.DisplayOrder == category.DisplayOrder))
        {
            ModelState.AddModelError("DisplayOrder", "Display order already in use");
        }

        if (ModelState.IsValid)
        {
            _dbContext.Categories.Add(new Category { Name = category.Name, DisplayOrder = category.DisplayOrder });
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        else
        {
            return View(category);
        }
    }
}
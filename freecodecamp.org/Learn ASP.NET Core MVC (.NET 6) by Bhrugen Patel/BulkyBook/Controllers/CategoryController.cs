using BulkyBook.Data;
using BulkyBook.Data.Models;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Controllers;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _dbContext;

    public CategoryController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult Index()
    {
        IEnumerable<CategoryViewModel> categories = _dbContext.Categories
            .Select(m => new CategoryViewModel { Name = m.Name, DisplayOrder = m.DisplayOrder })
            .OrderBy(c => c.DisplayOrder);

        return View(categories);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

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
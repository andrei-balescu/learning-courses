using BulkyBook.Data;
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

    public IActionResult Index()
    {
        IEnumerable<CategoryViewModel> categories = _dbContext.Categories
            .Select(m => new CategoryViewModel { Name = m.Name, DisplayOrder = m.DisplayOrder })
            .OrderBy(c => c.DisplayOrder);

        return View(categories);
    }
}
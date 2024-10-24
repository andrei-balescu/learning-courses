using BulkyBook.Data;
using BulkyBook.Data.Models;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Controllers;

/// <summary>Controller that manages categories.</summary>
public class CategoryController : Controller
{
    private const string PROP_NAME = "Name";
    private const string PROP_DISPLAY_ORDER = "DisplayOrder";
    private const string VALIDATION_NAME_EQUAL_DISPPLAY = "The Display Order cannot exactly match the name";
    private const string VALIDATION_NAME_UNIQUE = "Name already exists";
    private const string VALIDATION_DISPLAY_UNIQUE = "Display order already in use";
    

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
            .Select(m => new CategoryViewModel { Id = m.Id, Name = m.Name, DisplayOrder = m.DisplayOrder })
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
    /// <param name="categoryViewModel">The category to create.</param>
    /// <returns>Redirects to category index or back to category creation if any validation errors.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CategoryViewModel category)
    {
        if (string.Equals(category.Name, category.DisplayOrder.ToString(), StringComparison.CurrentCulture))
        {
            ModelState.AddModelError(PROP_NAME, VALIDATION_NAME_EQUAL_DISPPLAY);
        }

        if (_dbContext.Categories.Any(c => c.Name == category.Name))
        {
            ModelState.AddModelError(PROP_NAME, VALIDATION_NAME_UNIQUE);
        }

        if (_dbContext.Categories.Any(c => c.DisplayOrder == category.DisplayOrder))
        {
            ModelState.AddModelError(PROP_DISPLAY_ORDER, VALIDATION_DISPLAY_UNIQUE);
        }

        if (ModelState.IsValid)
        {
            _dbContext.Categories.Add(new Category { Name = category.Name, DisplayOrder = category.DisplayOrder });
            _dbContext.SaveChanges();
            // TempData only available for a single request (e.g. not available after refresh)
            TempData[SharedConstants.TEMPDATA_KEY_SUCCESS] = "Category created successfully";
            return RedirectToAction("Index");
        }
        else
        {
            return View(category);
        }
    }

    /// <summary>Edit a category</summary>
    /// <param name="id">ID of the category to edit.</param>
    /// <returns>The Category/Edit page</returns>
    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (!id.HasValue)
        {
            return NotFound();
        }

        Category? category = _dbContext.Categories.SingleOrDefault(c => c.Id == id);
        if (category == null)
        {
            return NotFound();
        }

        CategoryViewModel categoryViewModel = new CategoryViewModel { Id = category.Id, Name = category.Name, DisplayOrder = category.DisplayOrder };
        return View(categoryViewModel);
    }

    /// <summary>Updates a category</summary>
    /// <param name="categoryViewModel">The category to update.</param>
    /// <returns>Redirects to category index or back to category edit page if any validation errors.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(CategoryViewModel categoryViewModel)
    {
        if (string.Equals(categoryViewModel.Name, categoryViewModel.DisplayOrder.ToString(), StringComparison.CurrentCulture))
        {
            ModelState.AddModelError(PROP_NAME, VALIDATION_NAME_EQUAL_DISPPLAY);
        }

        if (_dbContext.Categories.Any(c => c.Id != categoryViewModel.Id && c.Name == categoryViewModel.Name))
        {
            ModelState.AddModelError(PROP_NAME, VALIDATION_NAME_UNIQUE);
        }

        if (_dbContext.Categories.Any(c => c.Id != categoryViewModel.Id && c.DisplayOrder == categoryViewModel.DisplayOrder))
        {
            ModelState.AddModelError(PROP_DISPLAY_ORDER, VALIDATION_DISPLAY_UNIQUE);
        }

        if (ModelState.IsValid)
        {
            _dbContext.Categories.Update(new Category 
                { 
                    Id = (int)categoryViewModel.Id,
                    Name = categoryViewModel.Name,
                    DisplayOrder =  categoryViewModel.DisplayOrder
                });
            _dbContext.SaveChanges();
            TempData[SharedConstants.TEMPDATA_KEY_SUCCESS] = "Category updated successfully";
            return RedirectToAction("Index");
        }
        else
        {
            return View(categoryViewModel);
        }
    }

    /// <summary>Display the delete category page.</summary>
    /// <param name="id">ID of the category to delete.</param>
    /// <returns>The Category/Delete page</returns>
    [HttpGet]
    public IActionResult Delete(int? id)
    {
        if (!id.HasValue)
        {
            return NotFound();
        }

        Category? category = _dbContext.Categories.SingleOrDefault(c => c.Id == id);
        if (category == null)
        {
            return NotFound();
        }

        CategoryViewModel categoryViewModel = new CategoryViewModel { Id = category.Id, Name = category.Name, DisplayOrder = category.DisplayOrder };
        return View(categoryViewModel);
    }

    /// <summary>Delete a category.</summary>
    /// <param name="id">ID of the category to delete.</param>
    /// <returns>Redirects to Category/Index page</returns>
    [HttpPost]
    public IActionResult DeleteConfirm(int? id)
    {
        if (!id.HasValue)
        {
            return NotFound();
        }

        Category? category = _dbContext.Categories.SingleOrDefault(c => c.Id == id);
        if (category == null)
        {
            return NotFound();
        }

        _dbContext.Categories.Remove(category);
        _dbContext.SaveChanges();
        TempData[SharedConstants.TEMPDATA_KEY_SUCCESS] = "Category deleted successfully";
        return RedirectToAction("Index");
    }
}
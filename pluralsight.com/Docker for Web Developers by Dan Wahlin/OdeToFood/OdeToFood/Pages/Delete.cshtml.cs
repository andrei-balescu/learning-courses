using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class DeleteModel : PageModel
    {
        private readonly IRestaurantData _restaurantData;

        public Restaurant Restaurant { get; set; }

        public DeleteModel(IRestaurantData restaurantData)
        {
            _restaurantData = restaurantData;
        }

        public IActionResult OnGet(int restaurantId)
        {
            IActionResult result;

            Restaurant = _restaurantData.GetById(restaurantId);
            if (Restaurant == null)
            {
                result = RedirectToPage("./NotFound");
            }
            else
            {
                result = Page();
            }

            return result;
        }

        public IActionResult OnPost(int restaurantId)
        {
            IActionResult result;

            Restaurant restaurant = _restaurantData.Delete(restaurantId);
            _restaurantData.Commit();

            if (restaurant == null)
            {
                result = RedirectToPage("./NotFound");
            }
            else
            {
                TempData["Message"] = $"{restaurant.Name} deleted";
                result = RedirectToPage("./Index");
            }

            return result;
        }
    }
}
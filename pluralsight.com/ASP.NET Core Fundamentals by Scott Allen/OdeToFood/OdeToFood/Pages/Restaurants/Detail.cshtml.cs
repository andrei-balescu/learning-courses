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
    public class DetailModel : PageModel
    {
        public Restaurant Restaurant { get; set; }

        private IRestaurantData _restaurantData;

        public DetailModel(IRestaurantData restaurantData)
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
                // produces the same result as this being a void method
                result = Page();
            }

            return result;
        }
    }
}
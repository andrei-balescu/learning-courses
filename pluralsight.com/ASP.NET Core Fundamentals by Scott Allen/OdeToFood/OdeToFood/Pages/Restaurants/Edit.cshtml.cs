using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        private IRestaurantData _restaurantData;

        private IHtmlHelper _htmlHelper;

        [BindProperty]
        public Restaurant Restaurant { get; set; }

        public IEnumerable<SelectListItem> Cuisines { get; set; }

        public EditModel(IRestaurantData restaurantData,
                         IHtmlHelper htmlHelper)
        {
            _restaurantData = restaurantData;
            _htmlHelper = htmlHelper;
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
                Cuisines = _htmlHelper.GetEnumSelectList<CuisineType>();

                result = Page();
            }

            return result;
        }

        public IActionResult OnPost()
        {
            Restaurant = _restaurantData.Update(Restaurant);
            _restaurantData.Commit();
            
            Cuisines = _htmlHelper.GetEnumSelectList<CuisineType>();

            return Page();
        }
    }
}
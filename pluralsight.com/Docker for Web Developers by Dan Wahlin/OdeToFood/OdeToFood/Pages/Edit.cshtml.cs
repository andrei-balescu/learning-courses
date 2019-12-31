using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

        private const string c_actionTakenMessageProperty = "Message";

        public EditModel(IRestaurantData restaurantData,
                         IHtmlHelper htmlHelper)
        {
            _restaurantData = restaurantData;
            _htmlHelper = htmlHelper;
        }

        public IActionResult OnGet(int? restaurantId)
        {
            IActionResult result;

            if (restaurantId.HasValue)
            {
                Restaurant = _restaurantData.GetById(restaurantId.Value);
            }
            else
            {
                Restaurant = new Restaurant();
            }

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
            IActionResult result;

            if (ModelState.IsValid)
            {
                const int c_noRestaurantId = 0;
                bool restaurantHasdId = Restaurant.Id != c_noRestaurantId;
                if (restaurantHasdId)
                {
                    Restaurant = _restaurantData.Update(Restaurant);
                    TempData[c_actionTakenMessageProperty] = "Restaurant updated!";
                }
                else
                {
                    Restaurant = _restaurantData.Add(Restaurant);
                    TempData[c_actionTakenMessageProperty] = "Restaurant created!";
                }
                _restaurantData.Commit();

                result = RedirectToPage("./Detail", 
                new {
                    RestaurantId = Restaurant.Id
                });
            }
            else
            {
                Cuisines = _htmlHelper.GetEnumSelectList<CuisineType>();
                result = Page();
            }
            
            return result;
        }
    }
}
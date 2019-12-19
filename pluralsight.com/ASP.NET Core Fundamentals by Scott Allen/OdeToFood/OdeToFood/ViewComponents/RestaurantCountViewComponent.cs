using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using OdeToFood.Data;

namespace OdeToFood.ViewComponents
{
    public class RestaurantCountViewComponent : ViewComponent
    {
        private readonly IRestaurantData _restaurantData;

        public RestaurantCountViewComponent(IRestaurantData restaurantData)
        {
            _restaurantData = restaurantData;
        }

        // works like a MVC controller
        public IViewComponentResult Invoke()
        {
            // unlike partial views, a view component can get its own data instead of depending on a model from the parent view
            int count = _restaurantData.GetCountOfRestaurants();

            // renders default view in /Shared/Components/<view name>/ folder
            ViewViewComponentResult result = View(count);
            return result;
        }
    }
}
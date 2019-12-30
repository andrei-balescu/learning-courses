using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class ListModel : PageModel
    {
        private readonly IConfiguration _configuration;

        private readonly IRestaurantData _restaurantData;
        private readonly ILogger<ListModel> _logger;

        [TempData]
        public string Message { get; set; }

        public IEnumerable<Restaurant> Restaurants { get; set; }

        // ensures property is bound to value in the request if present
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public ListModel(IConfiguration configuration, IRestaurantData restaurantData, ILogger<ListModel> logger)
        {
            _configuration = configuration;
            _restaurantData = restaurantData;
            _logger = logger;
        }

        public void OnGet()
        {
            _logger.LogInformation($"Retrieving restaurants. Search term: '{SearchTerm}'");
            
            if (string.IsNullOrEmpty(Message))
            {
                Message = _configuration["Message"];
            }
            Restaurants = _restaurantData.GetRestaurantsByName(SearchTerm);
        }
    }
}
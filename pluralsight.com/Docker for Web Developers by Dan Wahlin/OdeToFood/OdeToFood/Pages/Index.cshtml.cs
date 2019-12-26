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
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;

        private readonly IRestaurantData _restaurantData;
        private readonly ILogger<IndexModel> _logger;

        [TempData]
        public string Message { get; set; }

        public IEnumerable<Restaurant> Restaurants { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public IndexModel(IConfiguration configuration, IRestaurantData restaurantData, ILogger<IndexModel> logger)
        {
            _configuration = configuration;
            _restaurantData = restaurantData;
            _logger = logger;
        }

        public void OnGet()
        {
            _logger.LogInformation($"Retrieving restaurants. Search term: '{SearchTerm}'");
            
            Restaurants = _restaurantData.GetRestaurantsByName(SearchTerm);
        }
    }
}
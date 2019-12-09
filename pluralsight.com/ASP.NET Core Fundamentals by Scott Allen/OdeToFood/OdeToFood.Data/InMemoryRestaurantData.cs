using System;
using System.Collections.Generic;
using System.Linq;
using OdeToFood.Core;

namespace OdeToFood.Data
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        private List<Restaurant> _restaurants;

        public InMemoryRestaurantData()
        {
            _restaurants = new List<Restaurant>
            {
                new Restaurant
                {
                    Id = 1,
                    Name = "Scott's Pizza",
                    Location = "Maryland",
                    Cuisine = CuisineType.Italian
                },
                new Restaurant
                {
                    Id = 2,
                    Name = "Cinnamon Club",
                    Location = "California",
                    Cuisine = CuisineType.Indian
                },
                new Restaurant
                {
                    Id = 3,
                    Name = "La Costa",
                    Location = "Tijuana",
                    Cuisine = CuisineType.Mexican
                }
            };
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name = null)
        {
            bool ContainsText(string searchTarget, string searchTerm = null)
            {
                bool containsText = true;

                bool hasSearchTerm = !string.IsNullOrEmpty(searchTerm);
                if (hasSearchTerm)
                {
                    int searchIndex = searchTarget.IndexOf(searchTerm, StringComparison.InvariantCultureIgnoreCase);
                    containsText = searchIndex > -1;
                }

                return containsText;
            }

            IEnumerable<Restaurant> restaurants = from r in _restaurants
                                                  where ContainsText(r.Name, name)
                                                  orderby r.Name
                                                  select r;

            return restaurants;
        }
    }
}
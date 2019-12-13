using System.Collections.Generic;
// No suggestion to use namespace for Linq operations in VS Code 1.40.2
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OdeToFood.Core;

namespace OdeToFood.Data
{
    public class PostgreSqlRestaurantData : IRestaurantData
    {
        private readonly OdeToFoodDbContext _dbContext;

        public PostgreSqlRestaurantData(OdeToFoodDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            _dbContext.Add(newRestaurant);
            return newRestaurant;
        }

        public int Commit()
        {
            int savedRecords = _dbContext.SaveChanges();
            return savedRecords;
        }

        public Restaurant Delete(int id)
        {
            Restaurant restaurant = GetById(id);

            if (restaurant != null)
            {
                _dbContext.Remove(restaurant);
            }

            return restaurant;
        }

        public Restaurant GetById(int id)
        {
            // Can use _dbContext.Find(id) as well
            // Pass in primary key type
            Restaurant restaurant = _dbContext.Restaurants.Find(id);
            return restaurant;
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name)
        {
            IQueryable<Restaurant> query = from r in _dbContext.Restaurants
                                           // can only use code that is translatable to SQL with LINQ to SQL
                                           // where ContainsText(r.Name, name)
                                           where string.IsNullOrEmpty(name) || r.Name.Contains(name)
                                           orderby r.Name
                                           select r;
            return query;
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            EntityEntry<Restaurant> entity = _dbContext.Attach(updatedRestaurant);
            entity.State = EntityState.Modified;

            return updatedRestaurant;
        }
    }
}
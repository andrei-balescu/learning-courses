using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OdeToFood.Core;

namespace OdeToFood.Data
{
    public class PostgreSqlRestaurantData : IRestaurantData
    {
        private readonly OdeToFoodDbContext _db;

        public PostgreSqlRestaurantData(OdeToFoodDbContext dbContext)
        {
            _db = dbContext;
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            _db.Restaurants.Add(newRestaurant);
            return newRestaurant;
        }

        public int Commit()
        {
            int savedRecords = _db.SaveChanges();
            return savedRecords;
        }

        public Restaurant Delete(int id)
        {
            Restaurant restaurant = GetById(id);

            if (restaurant != null)
            {
                _db.Restaurants.Remove(restaurant);
            }

            return restaurant;
        }

        public Restaurant GetById(int id)
        {
            Restaurant restaurant = _db.Restaurants.Find(id);
            return restaurant;
        }

        public int GetCountOfRestaurants()
        {
            int restaurantCount = _db.Restaurants.Count();
            return restaurantCount;
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name)
        {
            IQueryable<Restaurant> query = from r in _db.Restaurants
                                           where string.IsNullOrEmpty(name) || r.Name.Contains(name)
                                           orderby r.Name
                                           select r;
            return query;
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            EntityEntry<Restaurant> entity = _db.Restaurants.Attach(updatedRestaurant);
            entity.State = EntityState.Modified;

            return updatedRestaurant;
        }
    }
}
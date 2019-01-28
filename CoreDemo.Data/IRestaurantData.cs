using CoreDemo.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CoreDemo.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetRestaurantsByName(string name=null);
        Restaurant GetRestaurantById(int id);
        Restaurant Update(Restaurant updatedRestaurant );
        Restaurant Add(Restaurant restaurant);
        Restaurant Delete(int id);
        int Commit();

    }

    public class InMemoryRestaurantData: IRestaurantData
    {
        List<Restaurant> restaurants;

        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>();
            restaurants.Add(new Restaurant() { Id = 1 , Name = "Aligattor" , Location = "Caxias do Sul", Cuisine = CuisineType.Italian  });
            restaurants.Add(new Restaurant() { Id = 2 , Name = "Boticão de Ouro", Location = "Caxias do Sul", Cuisine = CuisineType.None });
            restaurants.Add(new Restaurant() { Id = 3 , Name = "Paris", Location = "São Paulo", Cuisine = CuisineType.Mexican });
            restaurants.Add(new Restaurant() { Id = 4 , Name = "Labaredas", Location = "Farroupilha", Cuisine = CuisineType.Indian });

        }

        public IEnumerable<Restaurant> GetRestaurantsByName( string name = null )
        {
            return from r in restaurants
                   where string.IsNullOrEmpty(name) || r.Name.ToLower().StartsWith(name.ToLower())
                   orderby r.Name
                   select r;
        }

        public Restaurant GetRestaurantById(int id)
        {
            return (from r in restaurants
                   where r.Id == id
                   select r).FirstOrDefault();
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var restaurant = GetRestaurantById(updatedRestaurant.Id);

            if ( restaurant != null )
            {
                restaurant.Name = updatedRestaurant.Name;
                restaurant.Location = updatedRestaurant.Location;
                restaurant.Cuisine = updatedRestaurant.Cuisine;
            }
            
            return restaurant;


        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            restaurants.Add(newRestaurant);

            newRestaurant.Id = restaurants.Max(r => r.Id) + 1;

            return newRestaurant;
        }

        public Restaurant Delete(int restaurantId)
        {
            var restaurant = restaurants.FirstOrDefault(r => r.Id == restaurantId);

            restaurants.Remove(restaurant);

            return restaurant;
        }

        public int Commit()
        {
            return 0;
        }

    }

    public class SqlRestaurantData : IRestaurantData
    {
        private readonly RestaurantDbContext context;

        public SqlRestaurantData( RestaurantDbContext context )
        {
            this.context = context;
        }

        public Restaurant Add(Restaurant restaurant)
        {
            context.Add(restaurant);

            return restaurant;
        }

        public int Commit()
        {
           return context.SaveChanges();
        }

        public Restaurant Delete(int id)
        {
            var restaurant = GetRestaurantById(id);

            if (restaurant != null)
            {
                context.Restaurants.Remove(restaurant);
            }

            return restaurant;

        }

        public Restaurant GetRestaurantById(int id)
        {
            var restaurant = context.Restaurants.Find(id);

            return restaurant;
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name = null)
        {
            if ( !string.IsNullOrWhiteSpace(name)  )
             return context.Restaurants.Where(r => r.Name.StartsWith(name) ).ToList();

            return context.Restaurants.ToList(); ;
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var restaurant = GetRestaurantById(updatedRestaurant.Id);

            if ( restaurant != null )
            {
                restaurant.Name = updatedRestaurant.Name;
                restaurant.Location = updatedRestaurant.Location;
                restaurant.Cuisine = updatedRestaurant.Cuisine;

                context.Restaurants.Update(restaurant);
            }
            
            return restaurant;
        }
    }
}

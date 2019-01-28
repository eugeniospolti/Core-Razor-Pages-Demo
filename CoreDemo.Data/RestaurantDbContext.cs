using CoreDemo.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreDemo.Data
{
    public class RestaurantDbContext: DbContext
    {
        private readonly DbContextOptions<RestaurantDbContext> options;

        public RestaurantDbContext( DbContextOptions<RestaurantDbContext> options ): base(options)
        {
            this.options = options;
        }

        public DbSet<Restaurant> Restaurants { get; set; }
    }
}

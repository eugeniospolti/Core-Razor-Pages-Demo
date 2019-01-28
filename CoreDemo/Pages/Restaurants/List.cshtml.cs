using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreDemo.Core;
using CoreDemo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoreDemo.Pages.Restaurants
{
    public class ListModel : PageModel
    {
        private readonly IRestaurantData restaurantData;

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public ListModel(IRestaurantData restaurantData )
        {
            this.restaurantData = restaurantData;
        }

        public IEnumerable<Restaurant> Restaurants { get; set; }

        public void OnGet()
        {
            Restaurants = this.restaurantData.GetRestaurantsByName(this.SearchTerm);

        }

    }
}
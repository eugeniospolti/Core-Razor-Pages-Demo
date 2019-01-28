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
    public class DetailModel : PageModel
    {
        private readonly IRestaurantData restauranteData;

        [TempData]
        public string Message { get; set; }

        public Restaurant Restaurant { get; set; }

        public DetailModel(IRestaurantData restauranteData )
        {
            this.restauranteData = restauranteData;
        }

        public IActionResult OnGet(int restaurantId)
        {
            Restaurant =  this.restauranteData.GetRestaurantById(restaurantId);

            if ( Restaurant == null )
            {
                return RedirectToPage("../NotFound");
            }

            return Page();
        }
    }
}
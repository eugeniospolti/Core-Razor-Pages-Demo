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
    public class DeleteModel : PageModel
    {
        private readonly IRestaurantData restaurantData;

        public Restaurant Restaurant { get; set; }

        public DeleteModel( IRestaurantData restaurantData )
        {
            this.restaurantData = restaurantData;
        }

        public IActionResult OnGet(int restaurantId)
        {
            Restaurant = restaurantData.GetRestaurantById(restaurantId);

            if (Restaurant == null)
                return RedirectToPage("../NotFound");

            return Page();

        }

        public IActionResult OnPost(int restaurantId)
        {
            Restaurant = restaurantData.Delete(restaurantId);

            if (Restaurant == null)
                return RedirectToPage("../NotFound");

            restaurantData.Commit();

            TempData["Message"] = "${Restaurant.Name} was deleted!";
            return RedirectToPage("./List");

        }

    }
}
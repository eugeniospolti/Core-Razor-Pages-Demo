using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreDemo.Core;
using CoreDemo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoreDemo.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        private readonly IRestaurantData restauranteData;
        private readonly IHtmlHelper htmlHelper;

      

        [BindProperty]
        public Restaurant Restaurant { get; set; }
        public IEnumerable<SelectListItem> Cuisines { get; set; }

        public EditModel( IRestaurantData restauranteData , IHtmlHelper htmlHelper )
        {
            this.restauranteData = restauranteData;
            this.htmlHelper = htmlHelper;
        }

        public IActionResult OnGet(int? restaurantId)
        {
            CreateCuisinesItens();

            if ( restaurantId.HasValue  )
            {
                Restaurant = restauranteData.GetRestaurantById(restaurantId.Value);

                if (Restaurant != null)
                {
                    return Page();
                }

                return RedirectToPage("../NotFound");

            }
            else
            {
                Restaurant = new Restaurant();
                return Page();
            }

        }

        private void CreateCuisinesItens()
        {
            Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                CreateCuisinesItens();
                return Page();
            }

            if ( Restaurant.Id > 0  )
            {
               restauranteData.Update(Restaurant);
            }
            else
            {
                restauranteData.Add(Restaurant);
            }

            restauranteData.Commit();

            TempData["Message"] = "Restaurant Saved!";

            return RedirectToPage("./Detail", new { restaurantId = Restaurant.Id });
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CityBreaks.Web.Models;
using CityBreaks.Web.Services;

namespace CityBreaks.Web.Pages.Cities
{
    public class DetailsModel : PageModel
    {
        private readonly ICityService _cityService;
        public City? City { get; set; }

        public DetailsModel(ICityService cityService)
        {
            _cityService = cityService;
        }

        public async Task<IActionResult> OnGetAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
                return NotFound();

            City = await _cityService.GetByNameAsync(name);

            if (City == null)
                return NotFound();

            return Page();
        }
    }
}
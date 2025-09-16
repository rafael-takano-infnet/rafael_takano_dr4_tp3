using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CityBreaks.Web.Models;
using CityBreaks.Web.Services;

namespace CityBreaks.Web.Pages.Properties
{
    public class FilterModel : PageModel
    {
        private readonly IPropertyService _propertyService;

        public List<Property> Properties { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public decimal? MinPrice { get; set; }

        [BindProperty(SupportsGet = true)]
        public decimal? MaxPrice { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? CityName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? PropertyName { get; set; }

        public bool HasFilters => MinPrice.HasValue || MaxPrice.HasValue ||
                                  !string.IsNullOrEmpty(CityName) ||
                                  !string.IsNullOrEmpty(PropertyName);

        public FilterModel(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        public async Task OnGetAsync()
        {
            if (HasFilters)
            {
                Properties = await _propertyService.GetFilteredAsync(MinPrice, MaxPrice, CityName, PropertyName);
            }
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CityBreaks.Web.Data;
using CityBreaks.Web.Models;
using CityBreaks.Web.Services;
using System.ComponentModel.DataAnnotations;

namespace CityBreaks.Web.Pages.Properties
{
    public class CreateModel : PageModel
    {
        private readonly CityBreaksContext _context;
        private readonly IPropertyService _propertyService;

        [BindProperty]
        public PropertyInputModel Property { get; set; } = new();
        public SelectList CityList { get; set; } = new(new List<SelectListItem>(), "Value", "Text");

        public CreateModel(CityBreaksContext context, IPropertyService propertyService)
        {
            _context = context;
            _propertyService = propertyService;
        }

        public async Task<IActionResult> OnGetAsync(int? cityId)
        {
            await LoadCities();

            if (cityId.HasValue)
                Property.CityId = cityId.Value;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadCities();
                return Page();
            }

            var property = new Property
            {
                Name = Property.Name,
                PricePerNight = Property.PricePerNight,
                CityId = Property.CityId
            };

            await _propertyService.CreateAsync(property);

            var city = await _context.Cities.FindAsync(Property.CityId);
            if (city != null)
                return RedirectToPage("/Cities/Details", new { name = city.Name });

            return RedirectToPage("/Index");
        }

        private async Task LoadCities()
        {
            var cities = await _context.Cities
                .Include(c => c.Country)
                .OrderBy(c => c.Country.CountryName)
                .ThenBy(c => c.Name)
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = $"{c.Name} - {c.Country.CountryName}"
                })
                .ToListAsync();

            CityList = new SelectList(cities, "Value", "Text");
        }
    }

    public class PropertyInputModel
    {
        [Required(ErrorMessage = "Nome da propriedade é obrigatório")]
        [StringLength(200, ErrorMessage = "Nome deve ter no máximo 200 caracteres")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Preço por noite é obrigatório")]
        [Range(0.01, 9999.99, ErrorMessage = "Preço deve estar entre R$ 0,01 e R$ 9.999,99")]
        public decimal PricePerNight { get; set; }

        [Required(ErrorMessage = "Cidade é obrigatória")]
        public int CityId { get; set; }
    }
}
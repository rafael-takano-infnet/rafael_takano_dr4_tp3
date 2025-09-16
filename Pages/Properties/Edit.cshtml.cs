using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CityBreaks.Web.Models;
using CityBreaks.Web.Services;
using System.ComponentModel.DataAnnotations;

namespace CityBreaks.Web.Pages.Properties
{
    public class EditModel : PageModel
    {
        private readonly IPropertyService _propertyService;

        [BindProperty]
        public PropertyEditModel Property { get; set; } = new();

        public EditModel(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var property = await _propertyService.GetByIdAsync(id);

            if (property == null)
                return NotFound();

            Property = new PropertyEditModel
            {
                Id = property.Id,
                Name = property.Name,
                PricePerNight = property.PricePerNight,
                City = property.City
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var existingProperty = await _propertyService.GetByIdAsync(Property.Id);
            if (existingProperty == null)
                return NotFound();

            Property.City = existingProperty.City;

            if (!ModelState.IsValid)
                return Page();

            // Atualizar propriedades manualmente
            existingProperty.Name = Property.Name;
            existingProperty.PricePerNight = Property.PricePerNight;

            await _propertyService.UpdateAsync(existingProperty);

            return RedirectToPage("/Cities/Details", new { name = existingProperty.City.Name });
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            var existingProperty = await _propertyService.GetByIdAsync(Property.Id);
            if (existingProperty == null)
                return NotFound();

            await _propertyService.DeleteAsync(Property.Id);

            return RedirectToPage("/Cities/Details", new { name = existingProperty.City.Name });
        }
    }

    public class PropertyEditModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome da propriedade é obrigatório")]
        [StringLength(200, ErrorMessage = "Nome deve ter no máximo 200 caracteres")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Preço por noite é obrigatório")]
        [Range(0.01, 9999.99, ErrorMessage = "Preço deve estar entre R$ 0,01 e R$ 9.999,99")]
        public decimal PricePerNight { get; set; }

        public City City { get; set; } = null!;
    }
}


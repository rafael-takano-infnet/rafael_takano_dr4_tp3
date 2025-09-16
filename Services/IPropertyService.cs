using CityBreaks.Web.Models;

namespace CityBreaks.Web.Services
{
    public interface IPropertyService
    {
        Task<Property> CreateAsync(Property property);
        Task<Property?> GetByIdAsync(int id);
        Task<Property> UpdateAsync(Property property);
        Task DeleteAsync(int id);
        Task<List<Property>> GetFilteredAsync(decimal? minPrice, decimal? maxPrice, string? cityName, string? propertyName);
    }
}
using Microsoft.EntityFrameworkCore;
using CityBreaks.Web.Data;
using CityBreaks.Web.Models;

namespace CityBreaks.Web.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly CityBreaksContext _context;

        public PropertyService(CityBreaksContext context)
        {
            _context = context;
        }

        public async Task<Property> CreateAsync(Property property)
        {
            await _context.Properties.AddAsync(property);
            await _context.SaveChangesAsync();
            return property;
        }

        public async Task<Property?> GetByIdAsync(int id)
        {
            return await _context.Properties
                .Include(p => p.City)
                .ThenInclude(c => c.Country)
                .FirstOrDefaultAsync(p => p.Id == id && p.DeletedAt == null);
        }

        public async Task<Property> UpdateAsync(Property property)
        {
            Console.WriteLine($"=== PropertyService.UpdateAsync CHAMADO ===");
            Console.WriteLine($"ID: {property.Id}");
            Console.WriteLine($"Nome: {property.Name}");
            Console.WriteLine($"Preço: {property.PricePerNight}");

            // Buscar a propriedade existente no banco
            var existingProperty = await _context.Properties.FindAsync(property.Id);

            if (existingProperty == null)
            {
                Console.WriteLine($"Propriedade com ID {property.Id} não encontrada no banco!");
                throw new ArgumentException($"Propriedade com ID {property.Id} não foi encontrada");
            }

            Console.WriteLine($"Propriedade encontrada no banco: {existingProperty.Name}");

            // Atualizar apenas os campos necessários
            existingProperty.Name = property.Name;
            existingProperty.PricePerNight = property.PricePerNight;

            Console.WriteLine($"Valores após atualização: {existingProperty.Name} - R$ {existingProperty.PricePerNight}");

            try
            {
                var result = await _context.SaveChangesAsync();
                Console.WriteLine($"SaveChangesAsync retornou: {result} registros afetados");

                if (result > 0)
                {
                    Console.WriteLine("✅ Propriedade atualizada com sucesso no banco!");
                }
                else
                {
                    Console.WriteLine("⚠️ Nenhum registro foi alterado no banco");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erro ao salvar no banco: {ex.Message}");
                throw;
            }

            return existingProperty;
        }

        public async Task DeleteAsync(int id)
        {
            var property = await _context.Properties.FindAsync(id);
            if (property != null)
            {
                property.DeletedAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Property>> GetFilteredAsync(decimal? minPrice, decimal? maxPrice, string? cityName, string? propertyName)
        {
            var query = _context.Properties
                .Include(p => p.City)
                .ThenInclude(c => c.Country)
                .Where(p => p.DeletedAt == null)
                .AsQueryable();

            if (minPrice.HasValue)
                query = query.Where(p => p.PricePerNight >= minPrice);

            if (maxPrice.HasValue)
                query = query.Where(p => p.PricePerNight <= maxPrice);

            if (!string.IsNullOrEmpty(cityName))
                query = query.Where(p => EF.Functions.Collate(p.City.Name, "NOCASE").Contains(cityName));

            if (!string.IsNullOrEmpty(propertyName))
                query = query.Where(p => EF.Functions.Collate(p.Name, "NOCASE").Contains(propertyName));

            return await query.ToListAsync();
        }
    }
}
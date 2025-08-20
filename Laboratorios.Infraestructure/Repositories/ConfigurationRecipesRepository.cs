using Laboratorios.Core.Entities;
using Laboratorios.Core.Interfaces;
using Laboratorios.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Laboratorios.Infraestructure.Repositories
{
    public class ConfigurationRecipesRepository : IConfigurationRecipesRepository
    {
        private readonly LaboratoriosContext _context;

        public ConfigurationRecipesRepository(LaboratoriosContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ConfigurationRecipes>> GetConfigurationRecipes()
        {
            try
            {
                var configurations = await _context.ConfigurationRecipes.ToListAsync();
                return configurations;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public ConfigurationRecipes InsertConfigurationRecipes(ConfigurationRecipes configuration)
        {
            try
            {
                if (configuration.configurationId > 0)
                {
                    _context.Entry(configuration).State = EntityState.Modified;
                }
                else
                {
                    _context.ConfigurationRecipes.Add(configuration);
                }
                _context.SaveChanges();

                return configuration;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

        }
    }
}

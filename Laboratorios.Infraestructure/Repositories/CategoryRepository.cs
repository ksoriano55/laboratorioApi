using Laboratorios.Core.Entities;
using Laboratorios.Core.Interfaces;
using Laboratorios.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Laboratorios.Infraestructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly LaboratoriosContext _context;

        public CategoryRepository(LaboratoriosContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Category>> GetCategory()
        {
            try
            {
                var categorys = await _context.Category.ToListAsync();
                return categorys;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public Category InsertCategory(Category category)
        {
            try
            {
                if (category.categoryId > 0)
                {
                    _context.Entry(category).State = EntityState.Modified;
                }
                else
                {
                    _context.Category.Add(category);
                }
                _context.SaveChanges();

                return category;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

        }
    }
}

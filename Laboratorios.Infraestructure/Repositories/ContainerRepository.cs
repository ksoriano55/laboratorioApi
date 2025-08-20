using Laboratorios.Core.Entities;
using Laboratorios.Core.Interfaces;
using Laboratorios.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Laboratorios.Infraestructure.Repositories
{
    public class ContainerRepository : IContainerRepository
    {
        private readonly LaboratoriosContext _context;

        public ContainerRepository(LaboratoriosContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Container>> GetContainer()
        {
            try
            {
                var Containers = await _context.Container.ToListAsync();
                return Containers;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<Container> InsertContainer(Container Container)
        {
            try
            {
                if (Container.containerId > 0)
                {
                    _context.Entry(Container).State = EntityState.Modified;
                }
                else
                {
                    _context.Container.Add(Container);
                }
                await _context.SaveChangesAsync();

                return Container;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

        }
    }
}

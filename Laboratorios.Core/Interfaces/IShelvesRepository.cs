using Laboratorios.Core.Entities;

namespace Laboratorios.Core.Interfaces
{
    public interface IShelvesRepository
    {
        Task<IEnumerable<Shelves>> GetShelves();
        Shelves InsertShelves(Shelves Shelves);
    }
}

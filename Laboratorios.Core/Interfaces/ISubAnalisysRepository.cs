using Laboratorios.Core.Entities;

namespace Laboratorios.Core.Interfaces
{
    public interface ISubAnalisysRepository
    {
        Task<IEnumerable<SubAnalisys>> GetSubAnalisys();

        SubAnalisys insert(SubAnalisys SubAnalisys);
    }
}

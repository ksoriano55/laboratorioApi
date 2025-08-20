using Laboratorios.Core.Entities;
using Laboratorios.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorios.Core.Interfaces
{
    public interface IAnalisysRepository
    {
        Task<IEnumerable<Analisys>> GetAnalisys();
        Task<IEnumerable<AnalisysViewModel>> GetAnalisysAssigned();

        Analisys InsertAnalisys(Analisys analisys);
        Analisys EditAnalisys(Analisys analisys);

        Task<IEnumerable<Analisys>> GetAnalisysByArea(int areaId);
        Task<IEnumerable<Analisys>> GetAnalisysByNotArea(int areaId);
        Task<Area_Analisys> AssingAnalisys(int areaId, int analisysId, bool action);
        Task<AnalisysControls> AssingControls(AnalisysControls data);
        Task<IEnumerable<AnalisysControls>> GetAnalisysControls(int analisysId);
    }
}

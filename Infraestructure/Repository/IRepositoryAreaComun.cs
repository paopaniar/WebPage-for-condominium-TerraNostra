using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryAreaComun
    {
        IEnumerable<areaComun> GetAreaComun();
        areaComun GetAreaComunById(int id);
        areaComun Save(areaComun areaComun);
        IEnumerable<areaComun> GetAreasByTipo(int? tipo);
    }
}

using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceAreaComun
    {
        IEnumerable<areaComun> GetAreaComun();
        areaComun GetAreaComunById(int id);
        areaComun Save(areaComun areaComun);
    }
}

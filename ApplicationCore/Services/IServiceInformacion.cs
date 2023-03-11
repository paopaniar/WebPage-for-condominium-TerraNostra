using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceInformacion
    {
        IEnumerable<informacion> GetInformacion();
        informacion GetPlanInformacionById(int id);
        informacion Save(informacion informacion);
    }
}

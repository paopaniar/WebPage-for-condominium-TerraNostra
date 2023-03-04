using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryInformacion
    {
        IEnumerable<informacion> GetInformacion();
        informacion GetInformacionById(int id);
        informacion Save(informacion informacion, string[] selectedUsuarios);
    }
}

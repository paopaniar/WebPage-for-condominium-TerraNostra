    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infraestructure.Models;

namespace Infraestructure.Repository
{
    public interface IRepositoryResidencia
    {
        IEnumerable<residencia> GetResidencia();
        residencia GetResidenciaByID(int id);
        void DeleteResidencia(int id);
        IEnumerable<residencia> GetResidenciaByUsuario(int idUsuario);
        residencia Save(residencia residencia);

    }
}

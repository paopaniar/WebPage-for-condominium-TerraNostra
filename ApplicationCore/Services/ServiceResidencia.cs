using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infraestructure.Models;
using Infraestructure.Repository;

namespace ApplicationCore.Services
{
   public class ServiceResidencia : IServiceResidencia
    {
        public IEnumerable<residencia> GetResidencia()
        {
            IRepositoryResidencia repository = new RepositoryResidencia();
            return repository.GetResidencia();
        }

        public residencia GetResidenciaByID(int id)
        {
            IRepositoryResidencia repository = new RepositoryResidencia();
            return repository.GetResidenciaByID(id);
        }
        public IEnumerable<residencia> GetResidenciaByUsuario(int idUsuario)
        {
            IRepositoryResidencia repository = new RepositoryResidencia();
            return repository.GetResidenciaByUsuario(idUsuario);
        }

        public residencia Save(residencia residencia)
        {
            IRepositoryResidencia repository = new RepositoryResidencia();
            return repository.Save(residencia);
        }
    }
}

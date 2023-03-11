using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceInformacion : IServiceInformacion
    {
        public IEnumerable<informacion> GetInformacion()
        {
            IRepositoryInformacion repository = new RepositoryInformacion();
            return repository.GetInformacion();
        }

        public IEnumerable<informacion> GetInformacionByTipo(int id, int tipo)
        {
            IRepositoryInformacion repository = new RepositoryInformacion();
            return repository.GetInformacionByTipo(id, tipo);
        }

        public informacion GetPlanInformacionById(int id)
        {
            IRepositoryInformacion repository = new RepositoryInformacion();
            return repository.GetInformacionById(id);
        }

        public informacion Save(informacion informacion)
        {
            IRepositoryInformacion repository = new RepositoryInformacion();
            return repository.Save(informacion);
        }
    }
}

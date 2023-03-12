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

        public IEnumerable<informacion> GetInformacionByTipo( int tipo)
        {
            IRepositoryInformacion repository = new RepositoryInformacion();
            return repository.GetInformacionByTipo( tipo);
        }

        public informacion GetInformacionById(int id)
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

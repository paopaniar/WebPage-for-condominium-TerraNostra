using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceIncidente : IServiceIncidente
    {
        public IEnumerable<incidente> GetIncidente()
        {
            IRepositoryIncidencias repository = new RepositoryIncidente();
            return repository.GetIncidente();
        }

        public incidente GetIncidenteById(int id)
        {
            IRepositoryIncidencias repository = new RepositoryIncidente();
            return repository.GetIncidenteoById(id);
        }

        public IEnumerable<incidente> GetIncidentexEstado(int? estado)
        {
            IRepositoryIncidencias repository = new RepositoryIncidente();
            return repository.GetIncidentexEstado(estado);
        }

        public incidente Save(incidente incidente)
        {
            IRepositoryIncidencias repository = new RepositoryIncidente();
            return repository.Save(incidente);
        }
    }
}

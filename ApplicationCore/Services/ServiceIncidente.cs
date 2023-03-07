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

        public incidente Save(incidente incidente, string[] selectedUsuarios)
        {
            IRepositoryIncidencias repository = new RepositoryIncidente();
            return repository.Save(incidente, selectedUsuarios);
        }
    }
}

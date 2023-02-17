using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infraestructure.Models;

namespace ApplicationCore.Services
{
   public class ServiceResidencia : IServiceResidencia
    {
        public IEnumerable<residencia> GetResidencia()
        {
            IRepositoryResidencia repository = new RepositoryResidencia();
            return repository.GetResidencia();
        }
    }
}

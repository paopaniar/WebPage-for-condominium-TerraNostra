using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
	public class ServicePlanResidencia : IServicePlanResidencia
	{
        public IEnumerable<plan_residencia> GetEstadosByEstado(int id, int estado)
        {
            IRepositoryPlanResidencia repository = new RepositoryPlanResidencia();
            return repository.GetEstadoByEstado(id, estado);
        }

        public IEnumerable<plan_residencia> GetPlanResidencia()
		{
			IRepositoryPlanResidencia repository = new RepositoryPlanResidencia();
			return repository.GetPlanResidencia();
		}

       
        public plan_residencia GetPlanResidenciaByID(int id)
		{
			IRepositoryPlanResidencia repository = new RepositoryPlanResidencia();
			return repository.GetEstadoCuentaById(id);
		}

        IEnumerable<plan_residencia> IServicePlanResidencia.GetPlanResidenciaByID(int id)
        {
            throw new NotImplementedException();
        }
    }
}

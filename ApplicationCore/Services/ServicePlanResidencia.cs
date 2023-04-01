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

        public plan_residencia GetPlanResidenciaBy(int id)
        {
            IRepositoryPlanResidencia repository = new RepositoryPlanResidencia();
            return repository.GetPlanResidenciaBy(id);
        }

        public plan_residencia GetPlanResidenciaByID(int id) {
            IRepositoryPlanResidencia repository = new RepositoryPlanResidencia();
            return repository.GetEstadoCuentaById(id);
		}

        public List<plan_residencia> GetPlanResidenciaByMonthAndYear(int id, int mes, int year)
        {
            IRepositoryPlanResidencia repository = new RepositoryPlanResidencia();
            return repository.GetPlanResidenciaByMonthAndYear(id, mes, year);
        }

        public plan_residencia Guardar(plan_residencia plan_residencia)
        {
            IRepositoryPlanResidencia repository = new RepositoryPlanResidencia();
            return repository.Guardar(plan_residencia);
        }

        public plan_residencia Save(plan_residencia plan_residencia, string[] selectedResidencias, string[] selectedPlanes)
        {
            IRepositoryPlanResidencia repository = new RepositoryPlanResidencia();
            return repository.Save(plan_residencia, selectedResidencias, selectedPlanes);
        }

        IEnumerable<plan_residencia> IServicePlanResidencia.GetPlanResidenciaByID(int id)
        {
            throw new NotImplementedException();
        }
    }
}

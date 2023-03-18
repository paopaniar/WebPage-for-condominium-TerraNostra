using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
	public interface IRepositoryPlanResidencia
	{
		IEnumerable<plan_residencia> GetPlanResidenciaByID(int id);
		IEnumerable<plan_residencia> GetPlanResidencia();
		plan_residencia GetEstadoCuentaById(int id);		
		IEnumerable<plan_residencia> GetEstadoByEstado(int id, int estado);
		plan_residencia Save(plan_residencia plan_residencia, string[] selectedResidencias, string[] selectedPlanes);


	}
}

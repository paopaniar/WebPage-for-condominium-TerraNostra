using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
	public interface IServicePlanResidencia
	{
	
		IEnumerable<plan_residencia> GetPlanResidenciaByID(int id);
		plan_residencia GetPlanResidenciaBy(int id);
		plan_residencia Guardar(plan_residencia plan_residencia);
		IEnumerable<plan_residencia> GetPlanResidencia();
		IEnumerable<plan_residencia> GetEstadosByEstado(int id, int estado);
		plan_residencia Save(plan_residencia plan_residencia, string[] selectedResidencias, string[] selectedPlanes);

	}
}

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
		plan_residencia GetPlanResidenciaByID(int id);
		IEnumerable<plan_residencia> GetPlanResidencia();
		plan_residencia GetEstadoCuentaById(int id);
		plan_residencia GetPlanResidenciaByEnabled(int id);

	}
}

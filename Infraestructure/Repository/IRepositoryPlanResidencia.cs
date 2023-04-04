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
		List<plan_residencia> GetPlanResidenciaByMonthAndYear(int id, int mes, int year);
		plan_residencia GetPlanResidenciaBy(int id);
		plan_residencia Guardar(plan_residencia plan_residencia);
		IEnumerable<plan_residencia> GetEstadosMes(int? mes);
		IEnumerable<plan_residencia> GetEstadosCuentaxUsuarioxMes(int user, int? mes);
		IEnumerable<plan_residencia> GetReporteByEstado(int estado);
		IEnumerable<plan_residencia> GetReporteByResidenteByMes(int? mes, int? residente, int? estado);

	}

}

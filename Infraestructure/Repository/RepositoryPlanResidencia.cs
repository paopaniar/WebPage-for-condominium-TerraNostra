using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
	public class RepositoryPlanResidencia : IRepositoryPlanResidencia
	{
		public IEnumerable<plan_residencia> GetPlanResidencia()
		{
			throw new NotImplementedException();
		}

		public plan_residencia GetPlanResidenciaByID(int id)
		{
			throw new NotImplementedException();
		}
	}
}

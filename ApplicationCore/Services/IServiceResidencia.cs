using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
	public interface IServiceResidencia
	{
		IEnumerable<residencia> GetResidencia();
		IEnumerable<residencia> GetResidenciaByUsuario(int idUsuario);
		residencia Save(residencia residencia);

	}
}

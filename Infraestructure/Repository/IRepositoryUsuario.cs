using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
	public interface IRepositoryUsuario
	{
		IEnumerable<usuario> GetUsuario();
		usuario GetUsuarioByID(int id);
		usuario Save(usuario usuario);

	}
}

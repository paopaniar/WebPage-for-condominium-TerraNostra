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
		usuario GetUser(string email, string password);
		usuario GetUsuarioByID(int id);
		IEnumerable<usuario> GetUsuario();

	}
}

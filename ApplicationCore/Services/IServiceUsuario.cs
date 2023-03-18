using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
	public interface IServiceUsuario
	{
	
		usuario GetUsuarioByID(int id);
		usuario GetUsuario(string email, string password);
	}
}

﻿using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
	public interface IServiceUsuario
	{
		IEnumerable<usuario> GetUsuario();
		usuario GetUsuarioByID(int id);
		usuario GetUsuario(string email, string password);
		usuario Save(usuario usuario);
	}

}

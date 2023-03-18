using Infraestructure.Models;
using Infraestructure.Repository;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
	public class ServiceUsuario : IServiceUsuario
	{
        public usuario GetUsuarioByID(int id)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            usuario oUsuario = repository.GetUsuarioByID(id);
            // Desencriptar el password para presentarlo
            oUsuario.password = Cryptography.DecrypthAES(oUsuario.password);
            return oUsuario;
        }


        public usuario GetUser(string email, string password)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            // Encriptar el password para poder compararlo

            string cryptPassword = Cryptography.EncrypthAES(password);

            return repository.GetUser(email, cryptPassword);
        }

        public IEnumerable<usuario> GetUsuario()
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            return repository.GetUsuario();

        }

      

    }
}

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
            return repository.GetUsuarioByID(id);
        }

        public IEnumerable<usuario> GetUsuario()
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            return repository.GetUsuario();
        }

        public usuario GetUsuario(string email, string password)
        {
            usuario oUsuario = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oUsuario = ctx.usuario.
                     Where(p => p.Email.Equals(email) && p.password == password).
                    FirstOrDefault<usuario>();
                }
                if (oUsuario != null)
                    oUsuario = GetUsuarioByID(oUsuario.identificacion);
                return oUsuario;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }
    }
}

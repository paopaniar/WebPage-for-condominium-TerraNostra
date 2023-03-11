using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryInformacion : IRepositoryInformacion
    {
        public IEnumerable<informacion> GetInformacion()
        {
            IEnumerable<informacion> informacion = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Obtener todas las ordenes incluyendo el cliente y el usuario
                    informacion = ctx.informacion.Include("usuario1").ToList();

                }
                return informacion;

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
                throw new Exception(mensaje);
            }
        }

        public informacion GetInformacionById(int id)
        {
            informacion oInfo = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Obtener libro por ID incluyendo el autor y todas sus categorías
                    oInfo = ctx.informacion.
                        Where(l => l.id == id).
                        Include("usuario1").
                        FirstOrDefault();

                }
                return oInfo;
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

		//public informacion Save(informacion informacion, string[] selectedUsuarios)
		//{
		//	throw new NotImplementedException();
		//}

		public informacion Save(informacion informacion)
		{
			int retorno = 0;
			informacion oInformacion = null;

			using (MyContext ctx = new MyContext())
			{
				ctx.Configuration.LazyLoadingEnabled = false;
				oInformacion = GetInformacionById((int)informacion.id);
				IRepositoryUsuario _RepositoryUsuarios = new RepositoryUsuario();

				if (oInformacion == null)
				{

					//Insertar
					//Logica para agregar las categorias al libro
					
					//Insertar Libro
					ctx.informacion.Add(informacion);
					//SaveChanges
					//guarda todos los cambios realizados en el contexto de la base de datos.
					retorno = ctx.SaveChanges();
					//retorna número de filas afectadas
				}
				else
				{
					//Registradas: 1,2,3
					//Actualizar: 1,3,4

					//Actualizar incidente
					ctx.informacion.Add(informacion);
					ctx.Entry(informacion).State = EntityState.Modified;
					retorno = ctx.SaveChanges();

					//Logica para actualizar Categorias
					
				}
			}

			if (retorno >= 0)
				oInformacion = GetInformacionById((int)informacion.id);

			return oInformacion;
		}
	}
}

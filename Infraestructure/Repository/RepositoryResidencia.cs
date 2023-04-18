using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infraestructure.Models;
using Infraestructure.Utils;

namespace Infraestructure.Repository
{
    public class RepositoryResidencia : IRepositoryResidencia
    {
     

        public void DeleteResidencia(int id)
        {
            throw new NotImplementedException();
        }

		//public IEnumerable<residencia> GetResidencia()
		//{
		//	throw new NotImplementedException();
		//}

		public IEnumerable<residencia> GetResidencia()
		{
			IEnumerable<residencia> lista = null;
			try
			{


				using (MyContext ctx = new MyContext())
				{
					ctx.Configuration.LazyLoadingEnabled = false;
					
					lista = ctx.residencia.Include("usuario1").ToList();
					

				}
				return lista;
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

		public residencia GetResidenciaByID(int id)
        {
            residencia oLibro = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Obtener libro por ID incluyendo el autor y todas sus categorías
                    oLibro = ctx.residencia.
                        Where(l => l.id == id).
                        Include("usuario1").                      
                        FirstOrDefault();

                }
                return oLibro;
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

		public IEnumerable<residencia> GetResidenciaByUsuario(int idUsuario)
		{
			throw new NotImplementedException();
		}

        public residencia Save(residencia residencia)
        {
            int retorno = 0;
            residencia oResidencia = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oResidencia = GetResidenciaByID((int)residencia.id);


                if (oResidencia == null)
                {

                    ctx.residencia.Add(residencia);
                    //SaveChanges
                    //guarda todos los cambios realizados en el contexto de la base de datos.
                    retorno = ctx.SaveChanges();
                    //retorna número de filas afectadas
                }
                else
                {
                    //Registradas: 1,2,3
                    //Actualizar: 1,3,4

                    //Actualizar Libro
                    ctx.residencia.Add(residencia);
                    ctx.Entry(residencia).State = EntityState.Modified;
                    retorno = ctx.SaveChanges();

                }
            }

            if (retorno >= 0)
                oResidencia = GetResidenciaByID((int)residencia.id);

            return oResidencia;
        }
    }
    
    }

using System;
using System.Collections.Generic;
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

        public IEnumerable<residencia> GetResidencia()
        {
            IEnumerable<residencia> lista = null;
            try
            {


                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Obtener todos los libros incluyendo el autor
                    lista = ctx.residencia.ToList();

                   
                    //lista = ctx.residencia.Include(x => x.).ToList();

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
            throw new NotImplementedException();
        }

		public IEnumerable<residencia> GetResidenciaByUsuario(int idUsuario)
		{
			throw new NotImplementedException();
		}

    }
    }

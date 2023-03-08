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

		public IEnumerable<residencia> GetResidencia()
		{
			throw new NotImplementedException();
		}

		//public IEnumerable<residencia> GetResidencia()
		//{
		//    IEnumerable<residencia> lista = null;
		//    try
		//    {


		//        using (MyContext ctx = new MyContext())
		//        {
		//            ctx.Configuration.LazyLoadingEnabled = false;
		//            //preguntar a la profe por la logica
		//            //Da error en el frente cuando pongo estadoConverted 
		//            if (lista != null)
		//            {
		//                foreach (var r in lista)
		//                {
		//                    if (r.estado == 1)
		//                    {
		//                        r.estadoConverted = "Activa";
		//                    }

		//                }
		//            }


		//        //Obtener todos los libros incluyendo el autor
		//        lista = ctx.residencia.Include("usuario1").ToList();               
		//          //  lista = ctx.residencia.Include(x => x.usuario1.nombre).ToList();

		//        }
		//        return lista;
		//    }

		//    catch (DbUpdateException dbEx)
		//    {
		//        string mensaje = "";
		//        Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
		//        throw new Exception(mensaje);
		//    }
		//    catch (Exception ex)
		//    {
		//        string mensaje = "";
		//        Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
		//        throw;
		//    }
		//}

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

    }
    }

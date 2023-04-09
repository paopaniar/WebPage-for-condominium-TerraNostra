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
    public class RepositoryAreaComun : IRepositoryAreaComun
    {
        IEnumerable<areaComun> lista = null;
        public IEnumerable<areaComun> GetAreaComun()
        {
			IEnumerable<areaComun> lista = null;
			try
			{
				using (MyContext ctx = new MyContext())
				{
					ctx.Configuration.LazyLoadingEnabled = false;
					lista = ctx.areaComun.Include("reservacion").ToList();

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

        public areaComun GetAreaComunById(int id)
        {
            areaComun oAreaComun = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Obtener libro por ID incluyendo el autor y todas sus categorías
                    oAreaComun = ctx.areaComun.
                        Where(l => l.id == id).
                        Include("reservacion").
                        FirstOrDefault();

                }
                return oAreaComun;
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

        public IEnumerable<areaComun> GetAreasByTipo(int? tipo)
        {
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Obtener reservas por estado
                    if (tipo != null)
                    {
                        lista = ctx.areaComun.Include("reservacion").Where(l => l.id == tipo).ToList();
                    }
                    else
                    {
                        lista = ctx.areaComun.Include("reservacion").ToList();
                    }

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

        public areaComun Save(areaComun areaComun)
        {
            int retorno = 0;
            areaComun oAreaComun = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oAreaComun = GetAreaComunById((int)areaComun.id);


                if (oAreaComun == null)
                {

                    ctx.areaComun.Add(areaComun);
                    retorno = ctx.SaveChanges();
                }
                else
                {
                    //Registradas: 1,2,3
                    //Actualizar: 1,3,4

                    //Actualizar Libro
                    ctx.areaComun.Add(areaComun);
                    ctx.Entry(areaComun).State = EntityState.Modified;
                    retorno = ctx.SaveChanges();

                }
            }

            if (retorno >= 0)
                oAreaComun = GetAreaComunById((int)areaComun.id);

            return oAreaComun;
        }
    
    }
}

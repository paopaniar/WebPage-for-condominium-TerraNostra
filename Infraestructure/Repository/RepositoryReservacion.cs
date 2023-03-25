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
    public class RepositoryReservacion : IRepositoryReservacion
    {
        public IEnumerable<reservacion> GetReservacion()
        {
			IEnumerable<reservacion> lista = null;
			try
			{
				using (MyContext ctx = new MyContext())
				{
					ctx.Configuration.LazyLoadingEnabled = false;
					lista = ctx.reservacion.Include("usuario1").Include("areaComun").ToList();
					
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

        public reservacion GetReservacionById(int id)
        {
            reservacion oReservacion = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Obtener libro por ID incluyendo el autor y todas sus categorías
                    oReservacion = ctx.reservacion.
                        Where(l => l.id == id).
                        Include("usuario1").Include("areaComun").
                        FirstOrDefault();

                }
                return oReservacion;
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

        public reservacion Save(reservacion reservacion)
        {
            int retorno = 0;
            reservacion oReservacion = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oReservacion = GetReservacionById((int)reservacion.id);


                if (oReservacion == null)
                {

                    ctx.reservacion.Add(reservacion);
                    retorno = ctx.SaveChanges();
                    //retorna número de filas afectadas
                }
                else
                {
                    ctx.reservacion.Add(reservacion);
                    ctx.Entry(reservacion).State = EntityState.Modified;
                    retorno = ctx.SaveChanges();

                }
            }

            if (retorno >= 0)
                oReservacion = GetReservacionById((int)reservacion.id);

            return oReservacion;
        }


        public reservacion SaveEstado(reservacion reservacion)
        {
            reservacion oreservacion = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oreservacion = GetReservacionById((int)reservacion.id);

                if (oreservacion == null)
                {
                    ctx.reservacion.Add(reservacion);
                }
                else
                {
                    oreservacion.estado = reservacion.estado;
                    ctx.Entry(oreservacion).State = EntityState.Modified;
                }

                ctx.SaveChanges();
            }

            return oreservacion;
        }

    }

}

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
    public class RepositoryIncidente : IRepositoryIncidencias
    {
        IEnumerable<incidente> lista = null;
        public IEnumerable<incidente> GetIncidente()
        {
            IEnumerable<incidente> incidente = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Obtener todas las ordenes incluyendo el cliente y el usuario
                    incidente = ctx.incidente.Include("usuario1").ToList();

                }
                return incidente;

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

        public incidente GetIncidenteoById(int id)
        {
            incidente oIncidente = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Obtener libro por ID incluyendo el autor y todas sus categorías
                    oIncidente = ctx.incidente.
                        Where(l => l.id == id).
                        Include("usuario1").
                        FirstOrDefault();

                }
                return oIncidente;
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

        public IEnumerable<incidente> GetIncidentexEstado(int? estado)
        {
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    if (estado != null)
                    {
                        lista = ctx.incidente.Include("usuario1").
                         Where(l => l.estado == estado).ToList();
                    }
                    else
                    {
                        lista = ctx.incidente.Include("usuario1").ToList();
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

        public incidente Save(incidente incidente)
        {
            incidente oIncidente = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oIncidente = GetIncidenteoById((int)incidente.id);

                if (oIncidente == null)
                {
                    ctx.incidente.Add(incidente);
                }
                else
                {
                    oIncidente.estado = incidente.estado;
                    ctx.Entry(oIncidente).State = EntityState.Modified;
                }

                ctx.SaveChanges();
            }

            return oIncidente;
        }



    }
}

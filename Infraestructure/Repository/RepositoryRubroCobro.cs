using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Infraestructure.Repository
{
    public class RepositoryRubroCobro : IRepositoryRubroCobro
    {
        public IEnumerable<rubro_cobro> GetRubroCobro()
        {
            List<rubro_cobro> rubro = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Obtener todas las ordenes incluyendo el cliente y el usuario
                    rubro = ctx.rubro_cobro.ToList();

                }
                return rubro;

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

        public rubro_cobro GetRubroCobroById(int id)
        {
            rubro_cobro oRubro = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Obtener libro por ID incluyendo el autor y todas sus categorías
                    oRubro = ctx.rubro_cobro.Find(id);
                }
                return oRubro;
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

        public rubro_cobro Save(rubro_cobro rubro)
        {
            int retorno = 0;
            rubro_cobro oRubro = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oRubro = GetRubroCobroById((int)rubro.id);
                IRepositoryIncidencias _ReporitoryIndicencia = new RepositoryIncidente();

                if (oRubro == null)
                {

                    //Insertar
                    //Logica para agregar las categorias al libro

                    //Insertar Libro
                    ctx.rubro_cobro.Add(rubro);
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
                    ctx.rubro_cobro.Add(rubro);
                    ctx.Entry(rubro).State = EntityState.Modified;
                    retorno = ctx.SaveChanges();




                }
            }

            if (retorno >= 0)
                oRubro = GetRubroCobroById((int)rubro.id);

            return oRubro;
        }
    }
}

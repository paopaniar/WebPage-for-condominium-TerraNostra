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
            rubro_cobro oRubroCobro = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oRubroCobro = GetRubroCobroById((int)rubro.id);
                IRepositoryRubroCobro _RepositoryRubro = new RepositoryRubroCobro();

                if (oRubroCobro == null)
                {

                    
                   
                    //Insertar 
                    ctx.rubro_cobro.Add(rubro);
                    //SaveChanges
                    //guarda todos los cambios realizados en el contexto de la base de datos.
                    retorno = ctx.SaveChanges();
                    //retorna número de filas afectadas
                }
                else
                {
                   

                    //Actualizar Libro
                    ctx.rubro_cobro.Add(rubro);
                    ctx.Entry(rubro).State = EntityState.Modified;
                    retorno = ctx.SaveChanges();

                    //Logica para actualizar Categorias
                    
                }
            }

            if (retorno >= 0)
                oRubroCobro = GetRubroCobroById((int)rubro.id);

            return oRubroCobro;
        }
	}
}

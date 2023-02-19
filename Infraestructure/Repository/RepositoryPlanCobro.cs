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
    public class RepositoryPlanCobro: IRepositoryPlanCobro
    {
        public IEnumerable<plan_cobro> GetPlanCobro()
        {

            try
            {

                IEnumerable<plan_cobro> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.plan_cobro.ToList<plan_cobro>();
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

        public IEnumerable<plan_cobro> GetPlanCobroByUsuario(int idUsuario)
        {
            throw new NotImplementedException();
        }
    }
}

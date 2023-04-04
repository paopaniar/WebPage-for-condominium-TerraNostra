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
    public class RepositoryPlanCobro : IRepositoryPlanCobro
    {
        public IEnumerable<plan_cobro> GetPlanCobro()
        {
            IEnumerable<plan_cobro> plan = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Obtener todas las ordenes incluyendo el cliente y el usuario
                    plan = ctx.plan_cobro.Include("rubro_cobro").ToList();

                }
                return plan;

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

        public plan_cobro GetPlanCobroById(int id)
        {
            plan_cobro oPlan = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Obtener libro por ID incluyendo el autor y todas sus categorías
                    oPlan = ctx.plan_cobro.
                        Where(l => l.id == id).Include("rubro_cobro").FirstOrDefault();

                }
                return oPlan;
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



        public plan_cobro Save(plan_cobro planCobro, string[] selectedRubros)
        {
            int retorno = 0;
            plan_cobro oPlanCobro = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oPlanCobro = GetPlanCobroById((int)planCobro.id);
                IRepositoryRubroCobro _RepositoryRubro = new RepositoryRubroCobro();

                if (oPlanCobro == null)
                {
                    if (selectedRubros != null)
                    {
                        planCobro.rubro_cobro = new List<rubro_cobro>();
                        decimal totalSum = 0;
                        foreach (var rubro in selectedRubros)
                        {
                            var rubroToAdd = _RepositoryRubro.GetRubroCobroById(int.Parse(rubro));
                            ctx.rubro_cobro.Attach(rubroToAdd);
                            planCobro.rubro_cobro.Add(rubroToAdd);
                            totalSum += Convert.ToDecimal(rubroToAdd.monto);
                        }
                        planCobro.total = totalSum;
                    }
                    else if (oPlanCobro != null)
                    {
                        planCobro.total = oPlanCobro.total;
                    }

                    ctx.plan_cobro.Add(planCobro);
                    retorno = ctx.SaveChanges();
                }
                else
                {
                    ctx.plan_cobro.Add(planCobro);
                    ctx.Entry(planCobro).State = EntityState.Modified;
                    retorno = ctx.SaveChanges();
                    var selectRubroId = new HashSet<string>(selectedRubros);
                    if (selectedRubros != null)
                    {
                        ctx.Entry(planCobro).Collection(p => p.rubro_cobro).Load();
                        var newCategoriaForLibro = ctx.rubro_cobro
                         .Where(x => selectRubroId.Contains(x.id.ToString())).ToList();
                        planCobro.rubro_cobro = newCategoriaForLibro;

                        decimal totalSum = 0;
                        foreach (var rubro in newCategoriaForLibro)
                        {
                            totalSum += Convert.ToDecimal(rubro.monto);
                        }
                        planCobro.total = totalSum;

                        ctx.Entry(planCobro).State = EntityState.Modified;
                        retorno = ctx.SaveChanges();
                    }
                }
            }

            if (retorno >= 0)
                oPlanCobro = GetPlanCobroById((int)planCobro.id);
            oPlanCobro.total = planCobro.total;
            return oPlanCobro;
        }

    }

}

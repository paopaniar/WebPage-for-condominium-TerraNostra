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
                    plan = ctx.plan_cobro.ToList<plan_cobro>();

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
                        Where(l => l.id == id).FirstOrDefault();

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

	

		public plan_cobro Save(plan_cobro plan_Cobro, string[] selectedRubros)
		{
			int retorno = 0;
			plan_cobro oPlanCobro = null;

            using (MyContext ctx = new MyContext())
			{
				ctx.Configuration.LazyLoadingEnabled = false;
                oPlanCobro = GetPlanCobroById((int)plan_Cobro.id);
				IRepositoryRubroCobro _RepositoryRubros = new RepositoryRubroCobro();

				if (oPlanCobro == null)
				{
					if (selectedRubros != null)
					{
                        oPlanCobro.rubro_cobro = new List<rubro_cobro>();
						foreach (var rubro in selectedRubros)
						{
							var rubroToAdd = _RepositoryRubros.GetRubroCobroById(int.Parse(rubro));
							ctx.rubro_cobro.Attach(rubroToAdd); //sin esto, EF intentará crear una categoría
                            plan_Cobro.rubro_cobro.Add(rubroToAdd);// asociar a la categoría existente con el libro
						}
					}
                    ctx.plan_cobro.Add(plan_Cobro);
					retorno = ctx.SaveChanges();
					}
				else
				{
					ctx.plan_cobro.Add(plan_Cobro);
					ctx.Entry(plan_Cobro).State = EntityState.Modified;
					retorno = ctx.SaveChanges();

					//Logica para actualizar Categorias
					var selectedRubrosId = new HashSet<string>(selectedRubros);
					if (selectedRubros != null)
					{
						ctx.Entry(plan_Cobro).Collection(p => p.rubro_cobro).Load();
						var newRubroForPlan = ctx.rubro_cobro
						 .Where(x => selectedRubrosId.Contains(x.id.ToString())).ToList();
                        plan_Cobro.rubro_cobro = newRubroForPlan;

						ctx.Entry(plan_Cobro).State = EntityState.Modified;
						retorno = ctx.SaveChanges();
					}
				}
			}

			if (retorno >= 0)
                oPlanCobro = GetPlanCobroById((int)plan_Cobro.id);

			return oPlanCobro;
		}
	}
   
}

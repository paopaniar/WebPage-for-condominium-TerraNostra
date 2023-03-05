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
                        Where(l => l.id == id).
                        Include("rubro_cobro").
                        FirstOrDefault();

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

        public plan_cobro Save(plan_cobro pc, string[] selectedRubros)
        {
            int retorno = 0;
            plan_cobro oPc = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oPc = GetPlanCobroById((int)pc.id);
                IRepositoryRubroCobro _RepositoryRubros = new RepositoryRubroCobro();

                if (oPc == null)
                {

                    //Insertar
                    //Logica para agregar las categorias al libro
                    if (selectedRubros != null)
                    {

                        oPc.rubro_cobros = new List<rubro_cobro>();
                        foreach (var rubro in selectedRubros)
                        {
                            var rubroToAdd = _RepositoryRubros.GetRubroCobroById(int.Parse(rubro));
                            ctx.rubro_cobro.Attach(rubroToAdd); //sin esto, EF intentará crear una categoría
                            pc.rubro_cobros.Add(rubroToAdd);// asociar a la categoría existente con el libro


                        }
                    }
                    //Insertar Libro
                    ctx.plan_cobro.Add(pc);
                    //SaveChanges
                    //guarda todos los cambios realizados en el contexto de la base de datos.
                    retorno = ctx.SaveChanges();
                    //retorna número de filas afectadas
                }
                else
                {
                    //Registradas: 1,2,3
                    //Actualizar: 1,3,4

                    //Actualizar incidente
                    ctx.plan_cobro.Add(pc);
                    ctx.Entry(pc).State = EntityState.Modified;
                    retorno = ctx.SaveChanges();

                    //Logica para actualizar Categorias
                    var selectedRubrosId = new HashSet<string>(selectedRubros);
                    if (selectedRubros != null)
                    {
                        ctx.Entry(pc).Collection(p => p.rubro_cobros).Load();
                        var newPlanCobroForRubro = ctx.rubro_cobro
                         .Where(x => selectedRubrosId.Contains(x.id.ToString())).ToList();
                        pc.rubro_cobros= newPlanCobroForRubro;

                        ctx.Entry(pc).State = EntityState.Modified;
                        retorno = ctx.SaveChanges();
                    }
                }
            }

            if (retorno >= 0)
                oPc = GetPlanCobroById((int)pc.id);

            return oPc;
        }
    }
   
}

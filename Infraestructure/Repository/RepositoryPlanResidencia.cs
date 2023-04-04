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
	public class RepositoryPlanResidencia : IRepositoryPlanResidencia
    {
        IEnumerable<plan_residencia> lista = null;
        public IEnumerable<plan_residencia> GetEstadoByEstado(int id, int estado)
        {
            IEnumerable<plan_residencia> oPlanR = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Obtener libros por Autor
                    oPlanR = ctx.plan_residencia.
                        Where(p => p.residenciaId == id && p.estado == estado).
                         Include("residencia").
                        Include("residencia.usuario1").
                        Include("plan_cobro").ToList();

                }
                return oPlanR;
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

        public plan_residencia GetEstadoCuentaById(int id)
        {
            plan_residencia oPlan = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Obtener libro por ID incluyendo el autor y todas sus categorías
                    oPlan = ctx.plan_residencia.
                        Where(l => l.id == id).
                        Include("residencia").
                        Include("residencia.usuario1").
                        Include("plan_cobro").
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

        public IEnumerable<plan_residencia> GetEstadosCuentaxUsuarioxMes(int user, int? mes)
        {

            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    if (mes != null)
                    {
                        lista = ctx.plan_residencia.Include("residencia").Include("residencia.usuario1").Include("plan_cobro").
                         Where(l => l.residencia.usuario1.identificacion== user && l.fecha.Month == mes).ToList();
                    }
                    else
                    {
                        lista = ctx.plan_residencia.Include("residencia").Include("residencia.usuario1").Include("plan_cobro").
                                                 Where(l => l.residencia.usuario1.identificacion == user).ToList();
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

        public IEnumerable<plan_residencia> GetEstadosMes(int? mes)
        {

            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    if (mes != null)
                    {
                        lista = ctx.plan_residencia.Include("residencia").Include("residencia.usuario1").Include("plan_cobro").
                         Where(l => l.fecha.Month == mes).ToList();
                    }
                    else
                    {
                        lista = ctx.plan_residencia.Include("residencia").Include("residencia.usuario1").Include("plan_cobro").ToList();
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

        public IEnumerable<plan_residencia> GetEstadosPagados()
        {
          
            List<plan_residencia> plan = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Obtener todas las ordenes incluyendo el cliente y el usuario
                    plan = ctx.plan_residencia.
                       Where(l => l.estado == 1).
                       Include("residencia").ToList();

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

  

        public IEnumerable<plan_residencia> GetPlanResidencia()
		{
            List<plan_residencia> plan = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Obtener todas las ordenes incluyendo el cliente y el usuario
                    plan = ctx.plan_residencia.Include("residencia").Include("plan_cobro").Include("residencia.usuario1").ToList();

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

        public plan_residencia GetPlanResidenciaBy(int id)
        {
            plan_residencia oPlanResidencia = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Obtener libro por ID incluyendo el autor y todas sus categorías
                    oPlanResidencia = ctx.plan_residencia.
                        Include("residencia").Include("plan_cobro").Include("residencia.usuario1").
                        Where(l => l.id == id).FirstOrDefault();
                }
                return oPlanResidencia;
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

        public plan_residencia GetPlanResidenciaByID(int id)
		{
           plan_residencia oPlan = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Obtener libro por ID incluyendo el autor y todas sus categorías
                    oPlan = ctx.plan_residencia.
                        Include("residencia").Include("plan_cobro").Include("residencia.usuario1").
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

        public List<plan_residencia> GetPlanResidenciaByMonthAndYear(int id, int mes, int year)
        {
            using (var context = new MyContext())
            {
                return context.plan_residencia
                    .Where(pr => pr.fecha.Month == mes && pr.fecha.Year == year && pr.residenciaId == id)
                    .ToList();
            }
        }

        public IEnumerable<plan_residencia> GetReporteByEstado(int estado)
        {
            IEnumerable<plan_residencia> oPlanR = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Obtener libros por Autor
                    oPlanR = ctx.plan_residencia.
                        Where(p => p.estado == estado).
                         Include("residencia").
                        Include("residencia.usuario1").
                        Include("plan_cobro").ToList();

                }
                return oPlanR;
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

        public IEnumerable<plan_residencia> GetReporteByResidenteByMes(int? mes, int? residente, int? estado)
        {
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    if (mes != null && residente != null)
                    {
                        lista = ctx.plan_residencia.Include("residencia").Include("residencia.usuario1").Include("plan_cobro").
                         Where(l => l.residencia.numeroCasa == residente && l.fecha.Month == mes && l.estado == 0).ToList();
                    }
                    else
                    {
                        if (mes != null)
                        {
                            lista = ctx.plan_residencia.Include("residencia").Include("residencia.usuario1").Include("plan_cobro").
                         Where(l => l.fecha.Month == mes && l.estado == 0).ToList();
                        }
                        else
                        {
                            if (residente != null)
                            {
                                lista = ctx.plan_residencia.Include("residencia").Include("residencia.usuario1").Include("plan_cobro").
                                                        Where(l => l.residencia.numeroCasa == residente && l.estado == 0).ToList();
                            }
                            else
                            {
                                lista = ctx.plan_residencia.Include("residencia").Include("residencia.usuario1").Include("plan_cobro").
                                                        Where(l => l.estado == 0).ToList();
                            }
                        }
                           
                       
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

        public plan_residencia Guardar(plan_residencia plan_residencia)
        {
            plan_residencia oplan = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oplan = GetPlanResidenciaByID((int)plan_residencia.id);


                if (oplan == null)
                {
                    ctx.plan_residencia.Add(plan_residencia);
                }
                else
                {
                    oplan.estado = plan_residencia.estado;
                    ctx.Entry(oplan).State = EntityState.Modified;
                }

                ctx.SaveChanges();
            }

            return oplan;
    }

        public plan_residencia Save(plan_residencia plan_residencia, string[] selectedResidencias, string[] selectedPlanes)
        {
            int retorno = 0;
            plan_residencia oPlanResidencia = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oPlanResidencia = GetPlanResidenciaByID((int)plan_residencia.id);
                IRepositoryResidencia _RepositoryResidencia = new RepositoryResidencia();

                IRepositoryPlanCobro _RepositoryPlanes = new RepositoryPlanCobro();

                if (oPlanResidencia == null)
                {
                    if (selectedResidencias != null && selectedPlanes != null)
                    {
                        oPlanResidencia.plancobro = new List<plan_cobro>();
                        oPlanResidencia.residencias = new List<residencia>();

                        foreach (var residencia in selectedResidencias)
                        {
                            var ResidenciaToAdd = _RepositoryResidencia.GetResidenciaByID(int.Parse(residencia));
                            ctx.residencia.Attach(ResidenciaToAdd); //sin esto, EF intentará crear una categoría
                            plan_residencia.residencias.Add(ResidenciaToAdd);// asociar a la categoría existente con el libro

                        }
                        foreach (var plan in selectedPlanes)
                        {
                            var PlanToAdd = _RepositoryPlanes.GetPlanCobroById(int.Parse(plan));
                            ctx.plan_cobro.Attach(PlanToAdd); //sin esto, EF intentará crear una categoría
                            plan_residencia.plancobro.Add(PlanToAdd);// asociar a la categoría existente con el libro

                        }
                    }
                    ctx.plan_residencia.Add(plan_residencia);
      
                    retorno = ctx.SaveChanges();
                    //retorna número de filas afectadas
                }
                else
                {
                    ctx.plan_residencia.Add(plan_residencia);
                    ctx.Entry(plan_residencia).State = EntityState.Modified;
                    retorno = ctx.SaveChanges();
                    var selectResidencia = new HashSet<string>(selectedResidencias);
                    var selectPlan = new HashSet<string>(selectedPlanes);
                    if (selectedResidencias != null && selectedPlanes != null)
                    {
                        ctx.Entry(plan_residencia).Collection(p => p.residencias).Load();
                        var newPlanForResidencia = ctx.residencia
                         .Where(x => selectResidencia.Contains(x.id.ToString())).ToList();
                        plan_residencia.residencias = newPlanForResidencia;

                        ctx.Entry(plan_residencia).Collection(p => p.plancobro).Load();
                        var newResidenciaForPlan = ctx.plan_cobro
                         .Where(x => selectPlan.Contains(x.id.ToString())).ToList();
                        plan_residencia.plancobro = newResidenciaForPlan;

                        ctx.Entry(plan_residencia).State = EntityState.Modified;
                        retorno = ctx.SaveChanges();
                    }
                }
            }

            if (retorno >= 0)
                oPlanResidencia = GetPlanResidenciaByID((int)plan_residencia.id);

            return oPlanResidencia;
        }

        IEnumerable<plan_residencia> IRepositoryPlanResidencia.GetPlanResidenciaByID(int id)
        {
            throw new NotImplementedException();
        }
    }
    
}

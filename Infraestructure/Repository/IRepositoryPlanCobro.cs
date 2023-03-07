using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
   public interface IRepositoryPlanCobro
    {
        IEnumerable<plan_cobro> GetPlanCobro();
        IEnumerable<plan_cobro> GetPlanCobroByUsuario(int idUsuario);
        plan_cobro GetPlanCobroById(int id);
        plan_cobro Save(plan_cobro pc, string[] selectedRubros);
    }
}

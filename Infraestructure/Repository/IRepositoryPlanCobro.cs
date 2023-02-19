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
    }
}

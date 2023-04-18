using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServicePlanCobro
    {
        IEnumerable<plan_cobro> GetPlanCobro();
        plan_cobro GetPlanCobroById(int id);
        plan_cobro Save(plan_cobro plan_Cobro, string[] selectedRubros);
        void GetGrafico(out string etiquetas, out string valores);

    }
}

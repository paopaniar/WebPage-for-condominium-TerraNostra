using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infraestructure.Repository;

namespace ApplicationCore.Services
{
    public class ServicePlanCobro: IServicePlanCobro
    {
        public void GetGrafico(out string etiquetas1, out string valores1)
        {
            IRepositoryPlanCobro repository = new RepositoryPlanCobro();
            repository.GetGrafico(out string etiquetas, out string valores);
            etiquetas1 = etiquetas;
            valores1 = valores;
        }

        public IEnumerable<plan_cobro> GetPlanCobro()
        {
            IRepositoryPlanCobro repository = new RepositoryPlanCobro();
            return repository.GetPlanCobro();
        }

        public plan_cobro GetPlanCobroById(int id)
        {
            IRepositoryPlanCobro repository = new RepositoryPlanCobro();
            return repository.GetPlanCobroById(id);
        }

        public plan_cobro Save(plan_cobro plan_Cobro, string[] selectedRubros)
        {
            IRepositoryPlanCobro repository = new RepositoryPlanCobro();
            return repository.Save(plan_Cobro, selectedRubros);
        }
    }
}

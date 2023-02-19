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
    }
}

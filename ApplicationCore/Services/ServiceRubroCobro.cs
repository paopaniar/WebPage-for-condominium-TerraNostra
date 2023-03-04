using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceRubroCobro : IServiceRubroCobro
    {
        public IEnumerable<rubro_cobro> GetRubroCobro()
        {
            IRepositoryRubroCobro repository = new RepositoryRubroCobro();
            return repository.GetRubroCobro();
        }

        public rubro_cobro GetRubroCobroById(int id)
        {
            IRepositoryRubroCobro repository = new RepositoryRubroCobro();
            return repository.GetRubroCobroById(id);
        }

        public rubro_cobro Save(rubro_cobro rubro, string[] selectedUsuarios)
        {
            IRepositoryRubroCobro repository = new RepositoryRubroCobro();
            return repository.Save(rubro, selectedUsuarios);
        }
    }
}

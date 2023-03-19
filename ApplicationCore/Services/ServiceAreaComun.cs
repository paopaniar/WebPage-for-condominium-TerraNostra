using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceAreaComun : IServiceAreaComun
    {
        public IEnumerable<areaComun> GetAreaComun()
        {
            IRepositoryAreaComun repository = new RepositoryAreaComun();
            return repository.GetAreaComun();
        }

        public areaComun GetAreaComunById(int id)
        {
            IRepositoryAreaComun repository = new RepositoryAreaComun();
            return repository.GetAreaComunById(id);
        }

        public areaComun Save(areaComun areaComun)
        {
            IRepositoryAreaComun repository = new RepositoryAreaComun();
            return repository.Save(areaComun);
        }
    }
}

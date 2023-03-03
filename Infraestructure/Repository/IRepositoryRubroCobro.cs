using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
   public interface IRepositoryRubroCobro
    {
        IEnumerable<rubro_cobro> GetRubroCobro();
        rubro_cobro GetRubroCobroById(int id);
    }
}

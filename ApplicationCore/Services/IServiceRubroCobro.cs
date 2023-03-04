using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceRubroCobro
    {
        IEnumerable<rubro_cobro> GetRubroCobro();
        rubro_cobro GetRubroCobroById(int id);
        rubro_cobro Save(rubro_cobro rubro, string[] selectedUsuarios);
    }
}

using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceIncidente
    {
        IEnumerable<incidente> GetIncidente();
       incidente GetIncidenteById(int id);
       incidente Save(incidente incidente);
    }
}

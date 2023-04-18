using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceReservacion
    {
        IEnumerable<reservacion> GetReservacion();
        reservacion GetReservacionById(int id);
        reservacion Save(reservacion reservacion);
        reservacion SaveEstado(reservacion reservacion);
        IEnumerable<reservacion> GetReservacionesxUsuarioxEstado( int user, int? estado);
        IEnumerable<reservacion> GetReservacionesxEstado(int? estado);

    }
}

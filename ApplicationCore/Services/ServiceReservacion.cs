using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceReservacion : IServiceReservacion
    {
        public IEnumerable<reservacion> GetReservacion()
        {
            IRepositoryReservacion repository = new RepositoryReservacion();
            return repository.GetReservacion();
        }

        public reservacion GetReservacionById(int id)
        {
            IRepositoryReservacion repository = new RepositoryReservacion();
            return repository.GetReservacionById(id);
        }

        public reservacion Save(reservacion reservacion)
        {
            IRepositoryReservacion repository = new RepositoryReservacion();
            return repository.Save(reservacion);
        }


        public reservacion SaveEstado(reservacion reservacion)
        {
            IRepositoryReservacion repository = new RepositoryReservacion();
            return repository.SaveEstado(reservacion);
        }
    }
}

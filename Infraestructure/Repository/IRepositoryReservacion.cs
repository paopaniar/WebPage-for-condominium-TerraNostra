﻿using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryReservacion
    {
        IEnumerable<reservacion> GetReservacion();
        reservacion GetReservacionById(int id);
        reservacion Save(reservacion residencia);
    }
}

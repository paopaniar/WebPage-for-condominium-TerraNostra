﻿using Infraestructure.Models;
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
       
    }
}

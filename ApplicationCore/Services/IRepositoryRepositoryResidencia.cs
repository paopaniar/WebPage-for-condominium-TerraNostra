﻿using Infraestructure.Models;
using System.Collections.Generic;

namespace ApplicationCore.Services
{
    internal interface IRepositoryRepositoryResidencia
    {
        IEnumerable<residencia> GetResidencia();
    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Infraestructure.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(PlanResidenciaMetaData))]
    public partial class plan_residencia
    {
        public int id { get; set; }
        public int planCobroId { get; set; }
        public int residenciaId { get; set; }
        public string detalle { get; set; }
        public int estado { get; set; }

        
        public virtual plan_cobro plan_cobro { get; set; }
        public virtual residencia residencia { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Infraestructure.Models
{
    using System;
    using System.Collections.Generic;
    
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

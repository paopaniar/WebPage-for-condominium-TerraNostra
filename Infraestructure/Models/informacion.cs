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
    
    public partial class informacion
    {
        public int id { get; set; }
        public int usuario { get; set; }
        public string detalle { get; set; }
        public Nullable<System.DateTime> fechaInformacion { get; set; }
        public int tipo { get; set; }
        public Nullable<int> estado { get; set; }
        public string descTipo { get; set; }
    
        public virtual usuario usuario1 { get; set; }
    }
}

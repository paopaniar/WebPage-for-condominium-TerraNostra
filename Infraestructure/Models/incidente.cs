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
    
    public partial class incidente
    {
        public int id { get; set; }
        public int usuario { get; set; }
        public Nullable<int> estado { get; set; }
        public int tipo { get; set; }
        public string detalle { get; set; }
    
        public virtual usuario usuario1 { get; set; }
    }
}

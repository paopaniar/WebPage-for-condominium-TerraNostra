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
    
    public partial class residencia
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public residencia()
        {
            this.gestion_financiera = new HashSet<gestion_financiera>();
            this.plan_residencia = new HashSet<plan_residencia>();
        }
    
        public int id { get; set; }
        public int usuario { get; set; }
        public int individualsNumber { get; set; }
        public string annio { get; set; }
        public int estado { get; set; }
        public string otherInfoDetails { get; set; }
        public Nullable<int> numeroCasa { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<gestion_financiera> gestion_financiera { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<plan_residencia> plan_residencia { get; set; }
        public virtual usuario usuario1 { get; set; }
    }
}

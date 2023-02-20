using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infraestructure.Models
{
	internal partial class ResidenciaMetaData 
	{
        public int id { get; set; }
       
        [Display(Name = "Cantidad de personas")]
        public int individualsNumber { get; set; }
        [Display(Name = "Año")]
        public string annio { get; set; }
        [Display(Name = "Estado")]
        public int estado { get; set; }
        [Display(Name = "Información adicional")]
        public string otherInfoDetails { get; set; }
        [Display(Name = "Casa")]
        public int numeroCasa { get; set; }
        //[Display(Name = "Identificación de usuario")]
        //public int idUsuario { get; set; }
        public String estadoConverted()
        {
            if (this.estado==1)
            {
                return "Activo";
            }
            else
            {
                return "Inactivo";
            }
           
        }
    }

    internal partial class UsuarioMetaData
    {
        [Display(Name = "Rol")]
        public int rolId { get; set; }
        [Display(Name = "Residente")]
        public int identificacion { get; set; }
        [Display(Name = "Teléfono")]
        public string telefono { get; set; }
        [Display(Name = "Nombre")]
        public string nombre { get; set; }
        [Display(Name = "Primer Apellido")]
        public string apellido { get; set; }
        [Display(Name = "Segundo Apellido")]
        public string apellido2 { get; set; }
        [Display(Name = "Estado")]
        public Nullable<int> estado { get; set; }
        public virtual ICollection<residencia> residencia { get; set; }
    }

    internal partial class PlanCobroMetaData
    {

        
        [Display(Name = "Número plan cobro")]
        public int id { get; set; }
        [Display(Name = "Detalle")]
        public string detail { get; set; }
        [Display(Name = "Total")]
        public decimal total { get; set; }
        [Display(Name = "Residencia")]
        public int residenciaId { get; set; }
        [Display(Name = "Rubro")]
        public int rubroCobroId { get; set; }
       

    }

    internal partial class RubroComborMetaData
    {
      
        [Display(Name = "Detalle rubro")]
        public string detalle { get; set; }
        [Display(Name = "Monto")]
        public decimal monto { get; set; }

    }

}

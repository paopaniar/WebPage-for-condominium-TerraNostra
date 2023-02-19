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
        [Display(Name = "Identificación de usuario")]
        public int idUsuario { get; set; }
        [Display(Name = "Número de individuos")]
        public int individualsNumber { get; set; }
        [Display(Name = "Año")]
        public string annio { get; set; }
        [Display(Name = "Estado")]
        public int estado { get; set; }
        [Display(Name = "Información adicional")]
        public string otherInfoDetails { get; set; }
        [Display(Name = "Número de casa")]
        public Nullable<int> numeroCasa { get; set; }

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
        public int rolId { get; set; }
        public int identificacion { get; set; }
        public string telefono { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string apellido2 { get; set; }
        public Nullable<int> estado { get; set; }
        public virtual ICollection<residencia> residencia{ get; set; }
    }

}

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
        [RegularExpression(@"^\d+$", ErrorMessage = "{0} solo acepta números")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int individualsNumber { get; set; }
        [Display(Name = "Año")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string annio { get; set; }
        [Display(Name = "Estado")]
        public int estado { get; set; }
        [Display(Name = "Información adicional")]
        public string otherInfoDetails { get; set; }
        [Display(Name = "Casa")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int numeroCasa { get; set; }
        //[Display(Name = "Identificación de usuario")]
        //public int idUsuario { get; set; }
       
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
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string detail { get; set; }
        [Display(Name = "Total")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal total { get; set; }
        
        [Display(Name = "Rubro")]
        public int rubroCobroId { get; set; }
        [Display(Name = "Fecha")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public System.DateTime datePlan { get; set; }

    }

    internal partial class RubroCobroMetaData
    {
      
        [Display(Name = "Detalle rubro")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string detalle { get; set; }
        [Display(Name = "Monto")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal monto { get; set; }

    }
    internal partial class PlanResidenciaMetaData
    {

        [Display(Name = "Número plan cobro")]
        public int planCobroId { get; set; }
        [Display(Name = "Detalle")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string detalle { get; set; }
        [Display(Name = "Estado")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int estado { get; set; }

    }
    internal partial class IncidenteMetaData
    {
        [Display(Name = "Número de incidencia")]
        public int id { get; set; }
        [Display(Name = "Usuario")]
        public int usuario { get; set; }
        [Display(Name = "Estado")]
        public Nullable<int> estado { get; set; }
        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int tipo { get; set; }
        [Display(Name = "Detalle")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string detalle { get; set; }
    }

    internal partial class informacionMetaData
    {
        [Display(Name = "Número de seguimiento")]
        public int id { get; set; }
        [Display(Name = "Usuario")]
        public int usuario { get; set; }
        [Display(Name = "Detalle")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string detalle { get; set; }
        [Display(Name = "Fecha de la información")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public Nullable<System.DateTime> fechaInformacion { get; set; }
        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int tipo { get; set; }
        [Display(Name = "Estado")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int estado { get; set; }
    }

    }

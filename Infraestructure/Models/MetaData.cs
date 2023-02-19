using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infraestructure.Models
{
	internal partial class ResidenciaMetaData 
	{
        public int id { get; set; }
        public int idUsuario { get; set; }
        public int individualsNumber { get; set; }
        public string annio { get; set; }
        public int estado { get; set; }
        public string otherInfoDetails { get; set; }
        public Nullable<int> numeroCasa { get; set; }


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

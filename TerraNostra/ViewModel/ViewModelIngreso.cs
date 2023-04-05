using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TerraNostra.ViewModel
{
    public class ViewModelIngreso
    {
        public int id { get; set; }
        public string detail { get; set; }
        public string mes { get; set; }
        public System.DateTime datePlan { get; set; }
        public int estado { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Nullable<decimal> total { get { return calculoSubtotal(); } }
        public System.DateTime fecha { get; set; }
        
      //hcer logica
        private decimal? calculoSubtotal()
        {
            return total;
        }
        public virtual plan_cobro plan_cobro { get; set; }
        public virtual residencia residencia { get; set; }
        public virtual ICollection<plan_cobro> plancobro { get; set; }
        public virtual ICollection<residencia> residencias { get; set; }

        public ViewModelIngreso(int id)
        {
            IServicePlanCobro _ServiceLibro = new ServicePlanCobro();
            this.id = id;
            this.plan_cobro = _ServiceLibro.GetPlanCobroById(id);
        }
    }
   
}
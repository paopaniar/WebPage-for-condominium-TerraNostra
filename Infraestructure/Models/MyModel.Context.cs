﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class terraNostraEntities : DbContext
    {
        public terraNostraEntities()
            : base("name=terraNostraEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<areaComun> areaComun { get; set; }
        public virtual DbSet<gestion_financiera> gestion_financiera { get; set; }
        public virtual DbSet<incidente> incidente { get; set; }
        public virtual DbSet<informacion> informacion { get; set; }
        public virtual DbSet<plan_cobro> plan_cobro { get; set; }
        public virtual DbSet<plan_residencia> plan_residencia { get; set; }
        public virtual DbSet<reservacion> reservacion { get; set; }
        public virtual DbSet<residencia> residencia { get; set; }
        public virtual DbSet<rol> rol { get; set; }
        public virtual DbSet<rubro_cobro> rubro_cobro { get; set; }
        public virtual DbSet<usuario> usuario { get; set; }
    }
}

﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Proyecto.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class bolsaTrabajoEntities1 : DbContext
    {
        public bolsaTrabajoEntities1()
            : base("name=bolsaTrabajoEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<aplicacion_notas> aplicacion_notas { get; set; }
        public virtual DbSet<candidatos_ofertas> candidatos_ofertas { get; set; }
        public virtual DbSet<criterios> criterios { get; set; }
        public virtual DbSet<detalle_permisos> detalle_permisos { get; set; }
        public virtual DbSet<empleados> empleados { get; set; }
        public virtual DbSet<exp_profesional> exp_profesional { get; set; }
        public virtual DbSet<form_academica> form_academica { get; set; }
        public virtual DbSet<ofertas> ofertas { get; set; }
        public virtual DbSet<ofertas_criterios> ofertas_criterios { get; set; }
        public virtual DbSet<permisos> permisos { get; set; }
        public virtual DbSet<refer_personales> refer_personales { get; set; }
        public virtual DbSet<tipos_usuarios> tipos_usuarios { get; set; }
        public virtual DbSet<idiomas> idiomas { get; set; }
        public virtual DbSet<instituciones> instituciones { get; set; }
        public virtual DbSet<curriculum> curriculum { get; set; }
        public virtual DbSet<cv_exp_laboral> cv_exp_laboral { get; set; }
        public virtual DbSet<cv_form_academica> cv_form_academica { get; set; }
        public virtual DbSet<cv_idiomas> cv_idiomas { get; set; }
        public virtual DbSet<cv_ref_profesionales> cv_ref_profesionales { get; set; }
        public virtual DbSet<usuarios> usuarios { get; set; }
    }
}

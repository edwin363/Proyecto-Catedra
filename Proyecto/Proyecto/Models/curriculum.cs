//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class curriculum
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public curriculum()
        {
            this.candidatos_ofertas = new HashSet<candidatos_ofertas>();
        }
    
        public int id_curriculum { get; set; }
        public string codigo { get; set; }
        public string fotografia { get; set; }
        public int id_form_academica { get; set; }
        public int id_exp_laboral { get; set; }
        public int id_idiomas { get; set; }
        public int id_ref_profesionales { get; set; }
        public int id_usuario { get; set; }
        public decimal pretencion_salarial { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<candidatos_ofertas> candidatos_ofertas { get; set; }
        public virtual exp_profesional exp_profesional { get; set; }
        public virtual form_academica form_academica { get; set; }
        public virtual idiomas idiomas { get; set; }
        public virtual refer_personales refer_personales { get; set; }
        public virtual usuarios usuarios { get; set; }
    }
}

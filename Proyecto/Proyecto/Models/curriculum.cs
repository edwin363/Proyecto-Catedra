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
            this.cv_exp_laboral = new HashSet<cv_exp_laboral>();
            this.cv_form_academica = new HashSet<cv_form_academica>();
            this.cv_idiomas = new HashSet<cv_idiomas>();
            this.cv_ref_profesionales = new HashSet<cv_ref_profesionales>();

        }
    
        public int id_curriculum { get; set; }
        public int id_usuario { get; set; }
        public string fotografia { get; set; }
        public System.DateTime fechaNacimiento { get; set; }
        public string direccion { get; set; }
        public string numeroTelefono { get; set; }
        public string dui { get; set; }
        public string sexo { get; set; }
        public int estado { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<candidatos_ofertas> candidatos_ofertas { get; set; }
        public virtual usuarios usuarios { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cv_exp_laboral> cv_exp_laboral { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cv_form_academica> cv_form_academica { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cv_idiomas> cv_idiomas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cv_ref_profesionales> cv_ref_profesionales { get; set; }

    }
}

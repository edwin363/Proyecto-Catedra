//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Proyecto
{
    using System;
    using System.Collections.Generic;
    
    public partial class form_academica
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public form_academica()
        {
            this.curriculum = new HashSet<curriculum>();
        }
    
        public int id_form_academica { get; set; }
        public string tipo_educacion { get; set; }
        public System.DateTime fech_inicio { get; set; }
        public System.DateTime fech_fin { get; set; }
        public string titulo_obtenido { get; set; }
        public string institucion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<curriculum> curriculum { get; set; }
    }
}

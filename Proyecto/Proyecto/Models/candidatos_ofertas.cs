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
    
    public partial class candidatos_ofertas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public candidatos_ofertas()
        {
            this.aplicacion_notas = new HashSet<aplicacion_notas>();
        }
    
        public int id_cand_ofer { get; set; }
        public string descripcion { get; set; }
        public int id_oferta { get; set; }
        public int id_curriculum { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aplicacion_notas> aplicacion_notas { get; set; }
        public virtual curriculum curriculum { get; set; }
        public virtual ofertas ofertas { get; set; }
    }
}

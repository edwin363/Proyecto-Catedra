//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ofertas_criterios
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ofertas_criterios()
        {
            this.aplicacion_notas = new HashSet<aplicacion_notas>();
        }
    
        public int id_oferta_crit { get; set; }
        public int id_criterio { get; set; }
        public decimal porcentaje { get; set; }
        public int id_oferta { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aplicacion_notas> aplicacion_notas { get; set; }
        public virtual criterios criterios { get; set; }
        public virtual ofertas ofertas { get; set; }
    }
}
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
    
    public partial class usuarios
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public usuarios()
        {
            this.empleados = new HashSet<empleados>();
            this.curriculum = new HashSet<curriculum>();
        }
    
        public int id_usuario { get; set; }
        public string nombre { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public int id_tipo_usuario { get; set; }
        public string apellido1 { get; set; }
        public string apellido2 { get; set; }
        public int estado { get; set; }
        public string codigo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<empleados> empleados { get; set; }
        public virtual tipos_usuarios tipos_usuarios { get; set; }

        public virtual ICollection<curriculum> curriculum { get; set; }
    }
}

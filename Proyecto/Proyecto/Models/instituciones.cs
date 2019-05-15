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
    using System.ComponentModel.DataAnnotations;


    public partial class instituciones
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public instituciones()
        {
            this.empleados = new HashSet<empleados>();
        }

        public int id_institucion { get; set; }
        [Required(ErrorMessage = "Ingrese el Nombre de la institucion")]
        [Display(Name = "Nombre de la institución")]
        public string nombre { get; set; }
        [Required(ErrorMessage = "Ingrese el telefono")]
        [Display(Name = "Telefono")]
        public string telefono { get; set; }
        [Required(ErrorMessage = "Ingrese la dirección")]
        [Display(Name = "Dirección")]
        public string direccion { get; set; }
        [Required(ErrorMessage = "Ingrese el email")]
        [Display(Name = "Correo electronico")]
        public string email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<empleados> empleados { get; set; }
    }
}

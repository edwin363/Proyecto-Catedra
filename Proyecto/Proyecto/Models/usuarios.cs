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

    public partial class usuarios
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public usuarios()
        {
            this.curriculum = new HashSet<curriculum>();
            this.empleados = new HashSet<empleados>();
        }

        public int id_usuario { get; set; }

        [Required(ErrorMessage = "Ingrese el nombre del usuario")]
        [Display(Name = "Nombre")]
        public string nombre { get; set; }


        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "Ingrese la contraseña")]

        public string password { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Ingrese el email")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Correo no valido")]
        public string email { get; set; }

        [Display(Name = "Tipo de usuario")]
        [Required(ErrorMessage = "Ingrese el tipo de usuario")]
        public int id_tipo_usuario { get; set; }


        [Display(Name = "Primer Apellido")]
        [Required(ErrorMessage = "Ingrese el primer apellido")]
        public string apellido1 { get; set; }

        [Display(Name = "Segundo Apellido")]
        [Required(ErrorMessage = "Ingrese el segundo apellido")]
        public string apellido2 { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "Seleccione el estado")]
        public Nullable<int> estado { get; set; }
        public string codigo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<curriculum> curriculum { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<empleados> empleados { get; set; }
        public virtual tipos_usuarios tipos_usuarios { get; set; }
    }
}

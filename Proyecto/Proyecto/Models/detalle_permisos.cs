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
    
    public partial class detalle_permisos
    {
        public int id_detalle { get; set; }
        public int id_tipo_usuario { get; set; }
        public int id_permiso { get; set; }
    
        public virtual permisos permisos { get; set; }
        public virtual tipos_usuarios tipos_usuarios { get; set; }
    }
}

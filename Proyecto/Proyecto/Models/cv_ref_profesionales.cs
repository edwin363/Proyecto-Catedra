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
    
    public partial class cv_ref_profesionales
    {
        public int id_cv_ref_profesionales { get; set; }
        public int id_curriculum { get; set; }
        public int id_referencia { get; set; }
    
        public virtual curriculum curriculum { get; set; }
        public virtual refer_personales refer_personales { get; set; }
    }
}

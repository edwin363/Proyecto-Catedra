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

    public partial class aplicacion_notas
    {
        [Required(ErrorMessage = "Ingrese el id aplicaiones notas")]
        public int id_app_notas { get; set; }
        [Required(ErrorMessage = "Ingrese el id de las ofertas criterios")]
        public int id_ofer_crit { get; set; }
        [Required(ErrorMessage = "Ingrese el id candidato ofertas")]
        public int id_candi_ofer { get; set; }
        [Display(Name = "Nota")]
        [Required(ErrorMessage = "Ingrese la nota")]
        [Range(0, 10)]
        public decimal notas { get; set; }

        public virtual candidatos_ofertas candidatos_ofertas { get; set; }
        public virtual ofertas_criterios ofertas_criterios { get; set; }
    }
}

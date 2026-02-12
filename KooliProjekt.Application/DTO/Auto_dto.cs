using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.Application.DTO
{
    [ExcludeFromCodeCoverage]
    public class Auto_dto
    {
        public int Id { get; set; }
        [Required]
        public bool broneeritav { get; set; }
        [Required]
        [StringLength(32)]
        public string tüüp { get; set; }
    }
}

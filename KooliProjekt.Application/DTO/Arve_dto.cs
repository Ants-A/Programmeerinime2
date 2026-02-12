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
    public class Arve_dto
    {
        public int Id { get; set; }
        [Required]
        public int arve_omanik { get; set; }
        [Required]
        public int summa { get; set; }
        [Required]
        public int rendi_aeg { get; set; }
    }
}

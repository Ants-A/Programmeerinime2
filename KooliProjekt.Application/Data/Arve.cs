using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Data
{
    public class Arve : Entity
    {
        [Required]
        public int arve_omanik { get; set; }
        [Required]
        public int summa { get; set; }
        [Required]
        public int rendi_aeg { get; set; }
    }
}

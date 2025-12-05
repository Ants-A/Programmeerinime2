using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Data
{
    public class Auto : Entity
    {
        [Required]
        public bool broneeritav { get; set; }
        [Required]
        [StringLength(32)]
        public string tüüp { get; set; }
    }
}

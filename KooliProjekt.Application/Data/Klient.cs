using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Data
{
    public class Klient : Entity
    {
        [StringLength(64)]
        public string nimi { get; set; }
        [Required]
        [StringLength(64)]
        public string email { get; set; }
        public int phone { get; set; }
    }
}

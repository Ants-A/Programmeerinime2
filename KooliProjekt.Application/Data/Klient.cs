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
        public string nimi { get; set; }
        [Required]
        public string email { get; set; }
        public int phone { get; set; }
    }
}

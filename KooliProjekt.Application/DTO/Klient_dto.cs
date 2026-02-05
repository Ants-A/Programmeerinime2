using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.Application.DTO
{
    public class Klient_dto
    {
        public int Id { get; set; }
        [StringLength(32)]
        public string nimi { get; set; }
        [Required]
        [StringLength(32)]
        public string email { get; set; }
        [Required]
        public int phone { get; set; }
    }
}

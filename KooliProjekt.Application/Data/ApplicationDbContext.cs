using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Data
{
    public class Klient
    {
        public int id {  get; set; }
        public string nimi { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
    }

    public class Auto
    {
        public int id { get; set; }
        public bool broneeritav { get; set; }
        public string tüüp {  get; set; }
    }

    public class Arve
    {
        public int id { get; set; }
        public int arve_omanik { get; set; }
        public int summa {  get; set; }
        public int rendi_aeg { get; set; }
    }

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Klient> to_klient {  get; set; }
        public DbSet<Auto> to_auto { get; set; }
        public DbSet<Arve> to_arve { get; set; }
    }

}

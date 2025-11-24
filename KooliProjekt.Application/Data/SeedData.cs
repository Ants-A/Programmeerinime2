using KooliProjekt.Application.Features.Auto_;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Data
{
    /// <summary>
    /// 14.11.2025
    /// Testandmete generaator
    /// 
    /// Testandmed genereeritakse ainult siis kui mõni oluline 
    /// tabel on tühi.
    /// </summary>
    public class SeedData
    {
        private readonly ApplicationDbContext _db_context;

        public SeedData(ApplicationDbContext db_context)
        {
            _db_context = db_context;
        }

        /// <summary>
        /// Genereerib andmed
        /// </summary>
        public void Generate()
        {
            if(_db_context.to_arve.Any() && _db_context.to_auto.Any() && _db_context.to_klient.Any())
            {
                return;
            }

            Random ran = new Random();
            string[] auto_type = { "Maastur", "Linnamaastur", "Sport", "Mdea, midagi ägedat" };

            for (var i = 0; i < 10; i++)
            {
                var list_arve = new Arve { arve_omanik = ran.Next() , rendi_aeg = ran.Next(), summa = ran.Next() };
                _db_context.to_arve.Add(list_arve);
                var list_auto = new Auto { broneeritav = true, tüüp = auto_type[ran.Next(0, auto_type.Length)] };
                _db_context.to_auto.Add(list_auto);
                var list_klient = new Klient { email = "a.placeholder@email.com", nimi = "Mdea, pane midagi ägedat", phone = ran.Next(50000000, 59999999) };
                _db_context.to_klient.Add(list_klient);
            }

            _db_context.SaveChanges();
        }
    }
}

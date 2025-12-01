using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Data.Repositories
{
    // 28.11
    // ToDo listide repository klass
    public class klient_repository : BaseRepository<Klient>, i_klient_repository
    {
        public klient_repository(ApplicationDbContext dbContext) : 
            base(dbContext)
        {
        }

        // Lisa siia spetsiifilisemad meetodid,
        // mis on seotud ToDoListidega

        // BaseRepository ei tea, et Get peab tooma kaasa ka Itemsid
        public override async Task<Klient> GetByIdAsync(int id)
        {
            return await DbContext
                .to_klient
                .Where(list => list.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}

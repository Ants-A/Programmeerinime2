using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Data.Repositories
{
    // 28.11
    // ToDo listide repository interface (Program.cs failis
    // tuleb see ka ära regada)
    public interface i_auto_repository
    {
        Task<Auto> GetByIdAsync(int id);
        Task SaveAsync(Auto list);
        Task DeleteAsync(Auto entity);
    }
}

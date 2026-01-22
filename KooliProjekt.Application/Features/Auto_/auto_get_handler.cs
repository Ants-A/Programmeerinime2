using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using KooliProjekt.Application.DTO;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Auto_
{
    public class auto_get_handler : IRequestHandler<auto_get_query, OperationResult<Auto_dto>>
    {
        private readonly ApplicationDbContext _dbContext;

        public auto_get_handler(ApplicationDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            _dbContext = dbContext;
        }

        public async Task<OperationResult<Auto_dto>> Handle(auto_get_query request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Auto_dto>();

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.Id <= 0)
            {
                return result;
            }

            result.Value = await _dbContext
                .to_auto
                .Where(list => list.id == request.Id)
                .Select(list => new Auto_dto
                {
                    id = list.id,   
                    broneeritav = list.broneeritav,
                    tüüp = list.tüüp,
                })
                .FirstOrDefaultAsync();

            return result;
        }
    }
}

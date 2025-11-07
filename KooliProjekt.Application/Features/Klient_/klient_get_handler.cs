using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Klient_
{
    public class klient_get_handler : IRequestHandler<klient_get_query, OperationResult<object>>
    {
        private readonly ApplicationDbContext _dbContext;

        public klient_get_handler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<object>> Handle(klient_get_query request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<object>();

            result.Value = await _dbContext
                .to_klient
                .Where(list => list.id == request.Id)
                .FirstOrDefaultAsync();

            return result;
        }
    }
}

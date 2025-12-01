using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Auto_
{
    public class auto_get_handler : IRequestHandler<auto_get_query, OperationResult<object>>
    {
        private readonly i_auto_repository _dbContext;

        public auto_get_handler(i_auto_repository dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<object>> Handle(auto_get_query request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<object>();

            var list = await _dbContext.GetByIdAsync(request.Id);

            result.Value = new
            {
                Id = list.Id,
                broneeritav = list.broneeritav,
                tüüp = list.tüüp,
            };
            return result;
        }
    }
}

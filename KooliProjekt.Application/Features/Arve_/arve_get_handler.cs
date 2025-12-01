using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Features.Arve_;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Arve_
{
    public class arve_get_handler : IRequestHandler<arve_get_query, OperationResult<object>>
    {
        private readonly i_arve_repository _dbContext;

        public arve_get_handler(i_arve_repository dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<object>> Handle(arve_get_query request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<object>();
            var list = await _dbContext.GetByIdAsync(request.Id);

            result.Value = new
            {
                Id = list.Id,
                arve_omanik = list.arve_omanik,
                summa = list.summa,
                rendi_aeg = list.rendi_aeg
            };

            return result;
        }
    }
}

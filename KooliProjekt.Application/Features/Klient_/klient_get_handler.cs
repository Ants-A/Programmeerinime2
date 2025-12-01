using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Klient_
{
    public class klient_get_handler : IRequestHandler<klient_get_query, OperationResult<object>>
    {
        private readonly i_klient_repository _dbContext;

        public klient_get_handler(i_klient_repository dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<object>> Handle(klient_get_query request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<object>();
            var list = await _dbContext.GetByIdAsync(request.Id);

            result.Value = new
            {
                Id = list.Id,
                nimi = list.nimi,
                email = list.email,
                phone = list.phone,
            };

            return result;
        }
    }
}

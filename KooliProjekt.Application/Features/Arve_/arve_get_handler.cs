using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.Arve_;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using KooliProjekt.Application.DTO;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Arve_
{
    public class arve_get_handler : IRequestHandler<arve_get_query, OperationResult<Arve_dto>>
    {
        private readonly ApplicationDbContext _dbContext;

        public arve_get_handler(ApplicationDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            _dbContext = dbContext;
        }

        public async Task<OperationResult<Arve_dto>> Handle(arve_get_query request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Arve_dto>();

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.Id <= 0)
            {
                return result;
            }

            result.Value = await _dbContext
                .to_arve
                .Where(list => list.id == request.Id)
                .Select(list => new Arve_dto
                {
                    id = list.id,
                    arve_omanik = list.arve_omanik,
                    rendi_aeg = list.rendi_aeg,
                    summa = list.summa,
                })
                .FirstOrDefaultAsync();

            return result;
        }
    }
}

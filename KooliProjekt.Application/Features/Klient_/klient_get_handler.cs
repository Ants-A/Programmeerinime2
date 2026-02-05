using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using KooliProjekt.Application.DTO;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Klient_
{
    public class klient_get_handler : IRequestHandler<klient_get_query, OperationResult<Klient_dto>>
    {
        private readonly ApplicationDbContext _dbContext;

        public klient_get_handler(ApplicationDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            _dbContext = dbContext;
        }

        public async Task<OperationResult<Klient_dto>> Handle(klient_get_query request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Klient_dto>();

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.Id <= 0)
            {
                return result;
            }

            result.Value = await _dbContext
                .to_klient
                .Where(list => list.Id == request.Id)
                .Select(list => new Klient_dto
                {
                    Id = list.Id,
                    email = list.email,
                    nimi = list.nimi,
                    phone = list.phone,
                })
                .FirstOrDefaultAsync();

            return result;
        }
    }
}

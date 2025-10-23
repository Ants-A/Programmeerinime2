using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.Arve_;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Klient_
{
    public class klient_query_handler : IRequest<OperationResult<IList<Arve>>>
    {
        private readonly ApplicationDbContext _db_context;

        public klient_query_handler(ApplicationDbContext db_context)
        {
            _db_context = db_context;
        }

        public async Task<OperationResult<IList<Arve>>> Handle(klient_query request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<IList<Arve>>();
            result.Value = await _db_context
                .to_arve
                .OrderBy(list => list.id)
                .ToListAsync();

            return result;
        }
    }
}

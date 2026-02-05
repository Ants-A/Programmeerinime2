using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Arve_
{
    public class arve_query_handler : IRequestHandler<arve_query, OperationResult<PagedResult<Arve>>>
    {
        private readonly ApplicationDbContext _db_context;

        public arve_query_handler(ApplicationDbContext db_context)
        {
            if (db_context == null)
            {
                throw new ArgumentNullException(nameof(db_context));
            }
            _db_context = db_context;
        }

        public async Task<OperationResult<PagedResult<Arve>>> Handle(arve_query request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<Arve>>();

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.Page <= 0 || request.PageSize <= 0 || request.PageSize > request.MaxPageSize)
            {
                throw new ArgumentException(nameof(request));
                //return result;
            }

            result.Value = await _db_context
                .to_arve
                .OrderBy(list => list.Id)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}

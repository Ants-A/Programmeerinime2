using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.Arve_;
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

namespace KooliProjekt.Application.Features.Auto_
{
    public class auto_query_handler : IRequest<OperationResult<PagedResult<Auto>>>
    {
        private readonly ApplicationDbContext _db_context;

        public auto_query_handler(ApplicationDbContext db_context)
        {
            if (db_context == null)
            {
                throw new ArgumentNullException(nameof(db_context));
            }
            _db_context = db_context;
        }

        public async Task<OperationResult<PagedResult<Auto>>> Handle(auto_query request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<Auto>>();

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.Page <= 0)
            {
                return result;
            }

            result.Value = await _db_context
                .to_auto
                .OrderBy(list => list.Id)
                .GetPagedAsync(request.Page, request.PageSize); 

            return result;
        }
    }
}

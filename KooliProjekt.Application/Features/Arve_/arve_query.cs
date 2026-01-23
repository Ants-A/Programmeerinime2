using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Arve_
{
    public class arve_query : IRequest<OperationResult<PagedResult<Arve>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int MaxPageSize { get; set; } = 20;
    }
}

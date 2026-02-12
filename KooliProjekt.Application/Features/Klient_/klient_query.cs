using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Klient_ 
{
    [ExcludeFromCodeCoverage]
    public class klient_query : IRequest<OperationResult<IList<Klient>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}

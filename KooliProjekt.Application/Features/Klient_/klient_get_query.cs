using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Klient_
{
    public class klient_get_query : IRequest<OperationResult<object>>
    {
        public int Id { get; set; }
    }
}

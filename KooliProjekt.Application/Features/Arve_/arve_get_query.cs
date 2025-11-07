using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Arve_
{
    public class arve_get_query : IRequest<OperationResult<object>>
    {
        public int Id { get; set; }
    }
}

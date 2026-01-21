using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KooliProjekt.Application.DTO;

namespace KooliProjekt.Application.Features.Auto_
{
    public class auto_get_query : IRequest<OperationResult<Auto_dto>>
    {
        public int Id { get; set; }
    }
}

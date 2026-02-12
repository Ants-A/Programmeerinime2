using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Auto_
{
    [ExcludeFromCodeCoverage]
    public class auto_save_command : IRequest<OperationResult>, ITransactional
    {
        public int Id { get; set; }
        public bool broneeritav { get; set; }
        public string tüüp { get; set; }
    }
}

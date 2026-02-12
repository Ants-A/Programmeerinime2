using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Arve_
{
    [ExcludeFromCodeCoverage]
    public class arve_save_command : IRequest<OperationResult>, ITransactional
    {
        public int Id { get; set; }
        public int arve_omanik { get; set; }
        public int summa { get; set; }
        public int rendi_aeg { get; set; }
    }
}

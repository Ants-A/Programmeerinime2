using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Klient_
{
    public class klient_save_command : IRequest<OperationResult>, ITransactional
    {
        public int Id { get; set; }
        public string nimi { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
    }
}

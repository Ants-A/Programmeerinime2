using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Klient_
{
    public class klient_delete_command : IRequest<OperationResult>
    {
        public int Id { get; set; }
    }
}

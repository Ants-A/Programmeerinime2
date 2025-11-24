using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Auto_
{
    public class auto_delete_command : IRequest<OperationResult>
    {
        public int Id { get; set; }
    }
}

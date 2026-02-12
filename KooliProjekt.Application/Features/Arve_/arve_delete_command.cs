using System.Diagnostics.CodeAnalysis;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Arve_
{
    [ExcludeFromCodeCoverage]
    public class arve_delete_command : IRequest<OperationResult>
    {
        public int Id { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Auto_
{
    public class auto_delete_command_handler : IRequestHandler<auto_delete_command, OperationResult>
    {
        private readonly ApplicationDbContext _db_context;

        public auto_delete_command_handler(ApplicationDbContext db_context)
        {
            _db_context = db_context;
        }

        public async Task<OperationResult> Handle(auto_delete_command request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            await _db_context
                .to_auto
                .Where(t => t.Id == request.Id)
                .ExecuteDeleteAsync();

            return result;
        }
    }
}

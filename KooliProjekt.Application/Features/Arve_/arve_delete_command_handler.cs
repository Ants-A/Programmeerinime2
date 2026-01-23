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

namespace KooliProjekt.Application.Features.Arve_
{
    public class arve_delete_command_handler : IRequestHandler<arve_delete_command, OperationResult>
    {
        private readonly ApplicationDbContext _db_context;

        public arve_delete_command_handler(ApplicationDbContext db_context)
        {
            if (db_context == null)
            {
                throw new ArgumentNullException(nameof(db_context));
            }
            _db_context = db_context;
        }

        public async Task<OperationResult> Handle(arve_delete_command request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var result = new OperationResult();

            if (request.Id <= 0)
            {
                return result;
            }

            var arve = await _db_context
                .to_arve
                .Where(a => a.id == request.Id)
                .FirstOrDefaultAsync();

            if (arve == null)
            {
                return result;
            }

            _db_context.Remove(arve);

            await _db_context.SaveChangesAsync();

            return result;
        }
    }
}

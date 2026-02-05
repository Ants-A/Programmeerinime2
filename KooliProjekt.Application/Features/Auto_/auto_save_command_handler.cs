using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace KooliProjekt.Application.Features.Auto_
{
    public class auto_save_command_handler : IRequestHandler<auto_save_command, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public auto_save_command_handler(ApplicationDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(auto_save_command request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var result = new OperationResult();
            if (request.Id < 0)
            {
                result.AddPropertyError(nameof(request.Id), "Id cannot be negative");
                return result;
            }

            var list = new Auto();
            if(request.Id == 0)
            {
                await _dbContext.to_auto.AddAsync(list);
            }
            else
            {
                list = await _dbContext.to_auto.FindAsync(request.Id);
                if (list == null)
                {
                    result.AddError("Cannot find list with Id " + request.Id);
                    return result;
                }
            }

            list.broneeritav = request.broneeritav;
            list.tüüp = request.tüüp;
            
            await _dbContext.SaveChangesAsync();

            return result;
        }
    }
}

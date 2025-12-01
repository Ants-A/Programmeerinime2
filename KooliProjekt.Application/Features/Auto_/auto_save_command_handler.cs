using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace KooliProjekt.Application.Features.Auto_
{
    public class auto_save_command_handler : IRequestHandler<auto_save_command, OperationResult>
    {
        private readonly i_auto_repository _dbContext;

        public auto_save_command_handler(i_auto_repository dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(auto_save_command request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var list = new Auto();
            if (request.Id != 0)
            {
                list = await _dbContext.GetByIdAsync(request.Id);
            }

            list.Id = request.Id;
            list.broneeritav = request.broneeritav;
            list.tüüp = request.tüüp;
            
            await _dbContext.SaveAsync(list);

            return result;
        }
    }
}

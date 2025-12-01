using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Validators;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace KooliProjekt.Application.Features.Arve_
{
    public class arve_save_command_handler : IRequestHandler<arve_save_command, OperationResult>
    {
        private readonly i_arve_repository _dbContext;

        public arve_save_command_handler(i_arve_repository dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(arve_save_command request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var list = new Arve();
            if(request.Id != 0)
            {
                list = await _dbContext.GetByIdAsync(request.Id);
            }

            list.Id = request.Id;
            list.arve_omanik = request.arve_omanik;
            list.summa = request.summa;
            list.rendi_aeg = request.rendi_aeg;


            await _dbContext.SaveAsync(list);

            return result;
        }
    }
}

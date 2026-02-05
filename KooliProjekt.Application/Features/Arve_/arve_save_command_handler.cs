using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Validators;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace KooliProjekt.Application.Features.Arve_
{
    public class arve_save_command_handler : IRequestHandler<arve_save_command, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public arve_save_command_handler(ApplicationDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(arve_save_command request, CancellationToken cancellationToken)
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

            var list = new Arve();
            if(request.Id == 0)
            {
                await _dbContext.to_arve.AddAsync(list);
            }
            else
            {
                list = await _dbContext.to_arve.FindAsync(request.Id);
                if (list == null)
                {
                    result.AddError("Cannot find list with Id " + request.Id);
                    return result;
                }
            }

            list.arve_omanik = request.arve_omanik;
            list.summa = request.summa;
            list.rendi_aeg = request.rendi_aeg;


            await _dbContext.SaveChangesAsync();

            return result;
        }
    }
}

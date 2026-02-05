using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.Klient_;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Klient_
{
    public class klient_save_command_handler : IRequestHandler<klient_save_command, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public klient_save_command_handler(ApplicationDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(klient_save_command request, CancellationToken cancellationToken)
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

            var list = new Klient();
            if(request.Id == 0)
            {
                await _dbContext.to_klient.AddAsync(list);
            }
            else
            {
                list = await _dbContext.to_klient.FindAsync(request.Id);
                if (list == null)
                {
                    result.AddError("Cannot find list with Id " + request.Id);
                    return result;
                }
            }     
            
            list.phone = request.phone;
            list.email = request.email;
            list.nimi = request.nimi;

            await _dbContext.SaveChangesAsync();

            return result;
        }
    }
}

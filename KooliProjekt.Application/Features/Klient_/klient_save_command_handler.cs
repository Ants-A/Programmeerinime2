using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
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
        private readonly i_klient_repository _dbContext;

        public klient_save_command_handler(i_klient_repository dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(klient_save_command request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var list = new Klient();
            if (request.Id != 0)
            {
                list = await _dbContext.GetByIdAsync(request.Id);
            }

            list.Id = request.Id;
            list.phone = request.phone;
            list.email = request.email;
            list.nimi = request.nimi;

            await _dbContext.SaveAsync(list);

            return result;
        }
    }
}

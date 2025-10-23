using System.Threading.Tasks;
using KooliProjekt.Application.Features.Arve_;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.WebAPI.Controllers
{
    public class arve_controller : ApiControllerBase
    {
        private readonly IMediator _mediator;
        public arve_controller(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var query = new arve_query();
            var result = await _mediator.Send(query);

            return Result(result);
        }
    }
}

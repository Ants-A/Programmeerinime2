using System.Threading.Tasks;
using KooliProjekt.Application.Features.Auto_;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.WebAPI.Controllers
{
    public class auto_controller : ApiControllerBase
    {
        private readonly IMediator _mediator;
        public auto_controller(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List([FromQuery] auto_query query)
        {
            var result = await _mediator.Send(query);

            return Result(result);
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(int Id)
        {
            var query = new auto_get_query { Id = Id };
            var response = await _mediator.Send(query);

            return Result(response);
        }

        [HttpPost]
        [Route("Save")]
        public async Task<IActionResult> Save(auto_save_command command)
        {
            var response = await _mediator.Send(command);

            return Result(response);
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(auto_delete_command command)
        {
            var response = await _mediator.Send(command);

            return Result(response);
        }
    }
}

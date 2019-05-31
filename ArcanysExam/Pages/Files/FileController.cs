using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ArcanysExam.Pages.Files
{
    [Route("api/[controller]")]
    public class FileController : Controller
    {
        private readonly IMediator _mediator;

        public FileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult<List.Result>> Get()
            => await _mediator.Send(new List.Query());

        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]Create.Command command)
        {
            await _mediator.Send(command);

            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody]Delete.Command command)
        {
            await _mediator.Send(command);

            return Ok();
        }
    }
}

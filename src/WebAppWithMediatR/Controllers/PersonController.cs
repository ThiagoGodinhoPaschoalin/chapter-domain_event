using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAppWithMediatR.CQRS.Commands;
using WebAppWithMediatR.CQRS.Queries;

namespace WebAppWithMediatR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IMediator mediator;

        public PersonController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreatePersonCommand command)
        {
            try
            {
                return Created(HttpContext.Request.Path, await mediator.Send(command));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(Guid id)
        {
            try
            {
                return Ok(await mediator.Send(new GetPersonQuery(id)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await mediator.Send(new GetPeopleQuery()));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
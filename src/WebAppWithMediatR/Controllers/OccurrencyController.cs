using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAppWithMediatR.CQRS.Queries;

namespace WebAppWithMediatR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OccurrencyController : ControllerBase
    {
        private readonly IMediator mediator;

        public OccurrencyController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await mediator.Send(new GetOccurrenciesQuery()));
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
                return Ok(await mediator.Send(new GetOccurrencyQuery(id)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using WebAppWithReflectionAndEventDelegate.Facade;

namespace WebAppWithReflectionAndEventDelegate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OccurrencyController : ControllerBase
    {
        private readonly OccurrencyService occurrencyService;

        public OccurrencyController(OccurrencyService occurrencyService)
        {
            this.occurrencyService = occurrencyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await occurrencyService.GetAll());
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
                return Ok(await occurrencyService.GetOne(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
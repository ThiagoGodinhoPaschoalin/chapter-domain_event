using Microsoft.AspNetCore.Mvc;
using SharedDomain.Models;
using WebAppWithEventDelegateByGodinho.Facade;

namespace WebAppWithEventDelegateByGodinho.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly PersonService personService;

        /*
         * Porque o OccurrencyService está injetado aqui?
         * Se ele não estiver injetado, não será inicializado, então não conseguirá assinar o evento! ;) 
         */
        public PersonController(PersonService personService, OccurrencyService occurrencyService)
        {
            this.personService = personService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request">Não injetar o modelo diretamente! Aqui foi somente para focar na comunicação. Crie um REQUEST, e use AutoMapper para conversão de classe.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(PersonModel request)
        {
            try
            {
                return Created(HttpContext.Request.Path, await personService.Insert(request));
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
                return Ok(await personService.GetOne(id));
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
                return Ok(await personService.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

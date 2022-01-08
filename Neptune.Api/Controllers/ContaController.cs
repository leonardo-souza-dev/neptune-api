using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Neptune.Infra;
using Neptune.Domain;
using Neptune.Application;

namespace Neptune.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ContaController : ControllerBase
    {
        private readonly IContaService _contaService;

        public ContaController(IContaService contaService)
        {
            _contaService = contaService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_contaService.ObterTodas());
        }

        [HttpGet("{id}")]
        public IActionResult Obter(int id)
        {
            return Ok(_contaService.Obter(id));
        }

        //[HttpPost()]
        //public async Task<IActionResult> Criar([FromBody] ContaDomain conta)
        //{
        //    return Ok(await _contaService.Criar(conta));
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Atualizar([FromRoute] int id, [FromBody] ContaDomain conta)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != conta.Id)
        //    {
        //        return BadRequest("Id inválido");
        //    }

        //    return Ok(await _contaService.Atualizar(conta));
        //}
    }
}

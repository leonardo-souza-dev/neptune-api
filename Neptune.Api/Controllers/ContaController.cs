using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Neptune.Api.Services;
using Neptune.Models;

namespace Neptune.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ContaController : ControllerBase
    {
        private readonly IContaRepository ContaRepository;

        public ContaController(IContaRepository transacaoRepository)
        {
            ContaRepository = transacaoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await ContaRepository.ObterTodas());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Obter(int id)
        {
            return Ok(await ContaRepository.Obter(id));
        }

        [HttpPost()]
        public async Task<IActionResult> Criar([FromBody] Conta conta)
        {
            return Ok(await ContaRepository.Criar(conta));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar([FromRoute] int id, [FromBody] Conta conta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != conta.Id)
            {
                return BadRequest("Id inválido");
            }

            return Ok(await ContaRepository.Atualizar(conta));
        }
    }
}

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
    public class TransacaoController : ControllerBase
    {
        
        private readonly ILogger<TransacaoController> Logger;
        private readonly ITransacaoRepository TransacaoRepository;

        public TransacaoController(ILogger<TransacaoController> logger,
                                   ITransacaoRepository transacaoRepository)
        {
            Logger = logger;
            TransacaoRepository = transacaoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await TransacaoRepository.ObterTodas());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Obter(int id)
        {
            return Ok(await TransacaoRepository.Obter(id));
        }

        [HttpPost()]
        public async Task<IActionResult> Criar([FromBody] Transacao transacao)
        {
            return Ok(await TransacaoRepository.Criar(transacao));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar([FromRoute] int id, [FromBody] Transacao transacao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != transacao.Id)
            {
                return BadRequest("Id inválido");
            }

            return Ok(await TransacaoRepository.Atualizar(transacao));
        }
    }
}

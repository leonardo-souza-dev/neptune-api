using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Neptune.Application;
using Neptune.Domain;
using Neptune.Infra;

namespace Neptune.Api.Controllers
{
    //[ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TransacaoController : ControllerBase
    {
        private readonly ITransacaoService _transacaoService;

        public TransacaoController(ITransacaoService transacaoService)
        {
            _transacaoService = transacaoService;
        }


        [HttpGet("obter-mes")]
        public IActionResult ObterMes([FromQuery] int ano, int mes)
        {
            return Ok(_transacaoService.ObterMes(new MesTransacao(ano, mes)));
        }

        [HttpPost]
        public IActionResult Criar([FromBody] Transacao transacao)
        {
            return Ok(_transacaoService.Criar(transacao));
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar([FromRoute] int id, [FromBody] Transacao transacao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != transacao.Id)
            {
                return BadRequest("Id inválido");
            }

            return Ok(_transacaoService.Atualizar(transacao));
        }
    }
}

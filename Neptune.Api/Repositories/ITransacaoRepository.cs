using System.Collections.Generic;
using System.Threading.Tasks;
using Neptune.Models;

namespace Neptune.Api.Services
{
    public interface ITransacaoRepository
    {
        Task<List<Transacao>> ObterTodas();
        Task<Transacao> Obter(int id);
        Task<Transacao> Criar(Transacao transacao);
        Task<Transacao> Atualizar(Transacao transacao);
    }
}

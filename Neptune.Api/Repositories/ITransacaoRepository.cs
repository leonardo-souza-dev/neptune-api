using System.Collections.Generic;
using System.Threading.Tasks;
using Neptune.Models;

namespace Neptune.Api.Services
{
    public interface ITransacaoRepository
    {
        Task<List<TransacaoModel>> ObterTodas();
        Task<TransacaoModel> Obter(int id);
        Task<TransacaoModel> Criar(TransacaoModel transacao);
        Task<TransacaoModel> Atualizar(TransacaoModel transacao);
    }
}

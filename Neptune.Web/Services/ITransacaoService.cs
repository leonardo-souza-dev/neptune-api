using System.Collections.Generic;
using System.Threading.Tasks;
using Neptune.Models;

namespace Neptune.Web.Services
{
    public interface ITransacaoService
    {
        Task<IList<Transacao>> ObterTransacoes();
        Task<Transacao> ObterTransacao(int id);
        Task<Transacao> EditarTransacao(int id, Transacao transacao);
        Task<Transacao> NovaTransacao(Transacao transacao);
    }
}
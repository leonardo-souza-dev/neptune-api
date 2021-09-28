using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Neptune.Models;
using Neptune.Web.ViewModel;

namespace Neptune.Web.Services
{
    public interface ITransacaoService
    {
        Task<TransacoesViewModel> ObterTransacoesViewModel(DateTime data);
        Task<Transacao> ObterTransacao(int id);
        Task<Transacao> EditarTransacao(int id, Transacao transacao);
        Task<TransacaoViewModel> NovaTransacao(NovaTransacaoViewModel novaTransacaoViewModel);
    }
}
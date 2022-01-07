using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Neptune.Models;
using Neptune.Web.ViewModel;

namespace Neptune.Web.Services
{
    public interface ITransacaoService
    {
        Task<TransacoesMesViewModel> ObterTransacoesMesViewModel(int mes, int ano, List<int> contaId);
        Task<Transacao> ObterTransacao(int id);
        Task<Transacao> EditarTransacao(int id, Transacao transacao);
        Task<TransacaoViewModel> NovaTransacao(NovaTransacaoViewModel novaTransacaoViewModel);
        Task<List<ContaViewModel>> ObterContasViewModel();
    }
}
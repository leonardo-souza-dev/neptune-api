using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Neptune.Models;
using Neptune.Web.ViewModel;
using System;

namespace Neptune.Web.Services
{
    public class TransacaoService : ITransacaoService
    {
        public HttpClient HttpClient;

        public TransacaoService(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public async Task<TransacoesViewModel> ObterTransacoesViewModel(int mes, int ano)
        {
            var transacoes = await HttpClient.GetFromJsonAsync<List<Transacao>>("/api/transacao");
            var contaModel = await HttpClient.GetFromJsonAsync<Conta>("/api/conta/1"); //TODO: tirar essa ContaId = 1 hardcoded

            transacoes.Sort((x, y) => x.Data.CompareTo(y.Data));
            
            var transacoesMesPassadoPraTras = transacoes.Where(x => x.Data.Month < mes && x.Data.Year <= ano);
            var saldoMesAnterior = contaModel.SaldoInicial - transacoesMesPassadoPraTras.Where(x => x.ContaId == contaModel.Id).Sum(x => x.Valor);
            
            var transacoesModelMes = transacoes.Where(x => x.Data.Month == mes);

            var transacoesViewModel = new TransacoesViewModel(transacoesModelMes, saldoMesAnterior);

            return transacoesViewModel;
        }

        public async Task<Transacao> ObterTransacao(int id)
        {
            return await HttpClient.GetFromJsonAsync<Transacao>($"/api/transacao/{id}");
        }

        public async Task<Transacao> EditarTransacao(int id, Transacao transacao)
        {
            var response = await HttpClient.PutAsJsonAsync($"/api/transacao/{id}", transacao);
            return await response.Content.ReadFromJsonAsync<Transacao>();
        }

        public async Task<TransacaoViewModel> NovaTransacao(NovaTransacaoViewModel novaTransacaoViewModel)
        {
            var transacao = new Transacao
            {
                Data = novaTransacaoViewModel.Data,
                Descricao = novaTransacaoViewModel.Descricao,
                Valor = novaTransacaoViewModel.Valor,
                ContaId = novaTransacaoViewModel.ContaId
            };

            var response = await HttpClient.PostAsJsonAsync("/api/transacao", transacao);
            var transacaoModel = await response.Content.ReadFromJsonAsync<Transacao>();

            return new TransacaoViewModel(transacaoModel);
        }
    }
}
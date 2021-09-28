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

        public async Task<TransacoesViewModel> ObterTransacoesViewModel(DateTime data)
        {
            var transacoesModel = await HttpClient.GetFromJsonAsync<List<Transacao>>("/api/transacao");
            var transacoesModelMes = transacoesModel.Where(x => x.Data.Month == data.Month);

            var transacoesViewModel = new TransacoesViewModel(transacoesModelMes);

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
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Neptune.Models;

namespace Neptune.Web.Services
{
    public class TransacaoService : ITransacaoService
    {
        public HttpClient HttpClient;

        public TransacaoService(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public async Task<IList<Transacao>> ObterTransacoes()
        {
            return await HttpClient.GetFromJsonAsync<IList<Transacao>>("/api/transacao");
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

        public async Task<Transacao> NovaTransacao(Transacao transacao)
        {
            var response = await HttpClient.PostAsJsonAsync("/api/transacao", transacao);
            return await response.Content.ReadFromJsonAsync<Transacao>();
        }
    }
}
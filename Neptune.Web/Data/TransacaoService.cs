using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Neptune.Models;
using Neptune.Web.ViewModel;

namespace Neptune.Web.Data
{
    public class TransacaoService 
    {
        public HttpClient HttpClient;

        public TransacaoService(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public async Task<TransacoesMes> ObterTransacoesMes(int mes, int ano, List<int> contasId)
        {
            var transacoesModel = await ObterTransacoesModelSort();            
            var transacoesModelMes = transacoesModel.Where(x => x.Data.Month == mes);
            var saldoMesAnterior = await ObterSaldoMesAnterior(mes, ano, contasId, transacoesModel);

            var transacoesViewModel = new TransacoesMes(ano, mes, transacoesModelMes, saldoMesAnterior, await ObterContasModel());

            return transacoesViewModel;
        }

        public async Task<List<Conta>> ObterContas()
        {
            var contasModel = await HttpClient.GetFromJsonAsync<List<Conta>>("/api/conta");

            var contasViewModel = new List<Conta>();
            contasModel.ForEach(x => contasViewModel.Add(new Conta(x.Id, x.Nome, true)));

            return contasViewModel;
        }

        public async Task<TransacaoModel> ObterTransacao(int id)
        {
            return await HttpClient.GetFromJsonAsync<TransacaoModel>($"/api/transacao/{id}");
        }

        public async Task<TransacaoModel> EditarTransacao(int id, TransacaoModel transacao)
        {
            var response = await HttpClient.PutAsJsonAsync($"/api/transacao/{id}", transacao);
            return await response.Content.ReadFromJsonAsync<Models.TransacaoModel>();
        }

        public async Task<Transacao> NovaTransacao(NovaTransacao novaTransacaoViewModel)
        {
            var transacao = new TransacaoModel
            {
                Data = novaTransacaoViewModel.Data,
                Descricao = novaTransacaoViewModel.Descricao,
                Valor = novaTransacaoViewModel.Valor,
                ContaId = novaTransacaoViewModel.ContaId
            };

            var response = await HttpClient.PostAsJsonAsync("/api/transacao", transacao);
            var transacaoModel = await response.Content.ReadFromJsonAsync<TransacaoModel>();

            return new Transacao(transacaoModel);
        }










        private async Task<List<ContaModel>> ObterContasModel()
        {
            return await HttpClient.GetFromJsonAsync<List<ContaModel>>("/api/conta");
        }

        private async Task<decimal> ObterSaldoMesAnterior(int mes, int ano, List<int> contasId, List<Models.TransacaoModel> transacoes)
        {
            var contasModel = new List<ContaModel>();
            foreach (var contaId in contasId)
            {
                var contaModel2 = await HttpClient.GetFromJsonAsync<ContaModel>($"/api/conta/{contaId}");
                contasModel.Add(contaModel2);
            }
            var saldoInicialContas = contasModel.Sum(x => x.SaldoInicial);

            var transacoesMesPassadoPraTras = transacoes.Where(x => x.Data.Month < mes && x.Data.Year <= ano);
            var saldoMesAnterior = saldoInicialContas - transacoesMesPassadoPraTras.Where(x => contasId.Contains(x.ContaId)).Sum(x => x.Valor);

            return saldoMesAnterior;
        }

        private async Task<List<TransacaoModel>> ObterTransacoesModelSort()
        {
            var transacoes = await HttpClient.GetFromJsonAsync<List<TransacaoModel>>("/api/transacao");
            transacoes.Sort((x, y) => x.Data.CompareTo(y.Data));

            return transacoes;
        }






        #region HttpClientHelper

        //private async Task<T> Post<T>(T entidade)
        //{
        //    var url = $"https://localhost:21061/api/{nameof(T)}";
        //    var client = _clientFactory.CreateClient();
        //    var response = await client.PostAsJsonAsync(url, entidade);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var stringResponse = await response.Content.ReadAsStringAsync();

        //        return JsonSerializer.Deserialize<T>(stringResponse,
        //            new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        //    }
        //    else
        //    {
        //        throw new Exception($"Erro na chamada para {url}");
        //    }
        //}

        //private async Task<T> Put<T>(int id, T entidade)
        //{
        //    var url = $"https://localhost:21061/api/{nameof(T)}/{id}";
        //    var client = _clientFactory.CreateClient();
        //    var response = await client.PutAsJsonAsync(url, entidade);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var stringResponse = await response.Content.ReadAsStringAsync();

        //        return JsonSerializer.Deserialize<T>(stringResponse,
        //            new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        //    }
        //    else
        //    {
        //        throw new Exception($"Erro na chamada para {url}");
        //    }
        //}

        //private async Task<List<T>> GetList<T>()
        //{
        //    var url = $"https://localhost:21061/api/{nameof(T)}";
        //    var request = new HttpRequestMessage(HttpMethod.Get, url);
        //    var client = _clientFactory.CreateClient();
        //    var response = await client.SendAsync(request);
        //    var contasModel = new List<T>();
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var stringResponse = await response.Content.ReadAsStringAsync();

        //        contasModel = JsonSerializer.Deserialize<List<T>>(stringResponse,
        //            new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        //    }
        //    else
        //    {
        //        throw new Exception($"Erro na chamada para {url}");
        //    }

        //    return contasModel;
        //}

        //private async Task<T> GetItem<T>(int id)
        //{
        //    var url = $"https://localhost:21061/api/{nameof(T)}/{id}";
        //    var request = new HttpRequestMessage(HttpMethod.Get, url);
        //    var client = _clientFactory.CreateClient();
        //    var response = await client.SendAsync(request);
        //    var contasModel = new List<T>();
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var stringResponse = await response.Content.ReadAsStringAsync();

        //        return JsonSerializer.Deserialize<T>(stringResponse,
        //            new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        //    }
        //    else
        //    {
        //        throw new Exception($"Erro na chamada para {url}");
        //    }
        //}

        #endregion

    }
}
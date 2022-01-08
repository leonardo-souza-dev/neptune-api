using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Neptune.Domain;
using Neptune.Web.ViewModel;
using System;

namespace Neptune.Web.Data
{
    public class PagesService : IInteressado
    {
        public HttpClient HttpClient;
        public TransacoesMes TransacoesMes;

        public PagesService(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public async Task ObterTransacoesMesPage(int mes, int ano, List<int> contasId)
        {
            var transacoesModel = await ObterTransacoesModelSort();            
            var transacoesModelMes = transacoesModel.Where(x => x.Data.Month == mes);
            var saldoMesAnterior = await ObterSaldoMesAnterior(mes, ano, contasId, transacoesModel);

            var todasContasModel = await ObterContasModel();

            TransacoesMes = new TransacoesMes(ano,
                                              mes,
                                              transacoesModelMes,
                                              saldoMesAnterior,
                                              todasContasModel.Where(x => contasId.Contains(x.Id)).ToList(),
                                              todasContasModel);

            TransacoesMes.Contas.ForEach(x => x.AdicionarInteressado(this));
        }

        public async Task<List<Conta>> ObterContas()
        {
            var contasModel = await HttpClient.GetFromJsonAsync<List<ContaDomain>>("/api/conta");

            var contas = new List<Conta>();
            contasModel.ForEach(x => contas.Add(new Conta(x.Id, x.Nome, true)));

            return contas;
        }

        public async Task<Transacao> ObterTransacao(int id)
        {
            var transacaoDomain = await HttpClient.GetFromJsonAsync<TransacaoDomain>($"/api/transacao/{id}");

            return new Transacao(transacaoDomain);
        }

        public async Task<TransacaoDomain> EditarTransacao(int id, Transacao transacao)
        {
            var transacaoDomain = new TransacaoDomain(transacao.Id, transacao.Data, transacao.Descricao, transacao.Valor, transacao.ContaId);
            var response = await HttpClient.PutAsJsonAsync($"/api/transacao/{id}", transacaoDomain);

            return await response.Content.ReadFromJsonAsync<TransacaoDomain>();
        }

        public async Task<Transacao> NovaTransacao(NovaTransacao novaTransacaoViewModel)
        {
            var transacao = new TransacaoDomain
            {
                Data = novaTransacaoViewModel.Data,
                Descricao = novaTransacaoViewModel.Descricao,
                Valor = novaTransacaoViewModel.Valor,
                ContaId = novaTransacaoViewModel.ContaId
            };

            var response = await HttpClient.PostAsJsonAsync("/api/transacao", transacao);
            var transacaoModel = await response.Content.ReadFromJsonAsync<TransacaoDomain>();

            return new Transacao(transacaoModel);
        }





        public async Task Atualizar()
        {
            var contas = TransacoesMes.Contas.Where(x => x.Ativo).Select(x => x.Id).ToList();

            await ObterTransacoesMesPage(TransacoesMes.Mes,
                                     TransacoesMes.Ano,
                                     contas);
        }






        private async Task<List<ContaDomain>> ObterContasModel()
        {
            return await HttpClient.GetFromJsonAsync<List<ContaDomain>>("/api/conta");
        }

        private async Task<decimal> ObterSaldoMesAnterior(int mes, int ano, List<int> contasId, List<TransacaoDomain> transacoes)
        {
            var contasModel = new List<ContaDomain>();
            foreach (var contaId in contasId)
            {
                var contaModel2 = await HttpClient.GetFromJsonAsync<ContaDomain>($"/api/conta/{contaId}");
                contasModel.Add(contaModel2);
            }
            var saldoInicialContas = contasModel.Sum(x => x.SaldoInicial);

            var transacoesMesPassadoPraTras = transacoes.Where(x => x.Data.Month < mes && x.Data.Year <= ano);
            var saldoMesAnterior = saldoInicialContas - transacoesMesPassadoPraTras.Where(x => contasId.Contains(x.ContaId)).Sum(x => x.Valor);

            return saldoMesAnterior;
        }

        private async Task<List<TransacaoDomain>> ObterTransacoesModelSort()
        {
            var transacoes = await HttpClient.GetFromJsonAsync<List<TransacaoDomain>>("/api/transacao");
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
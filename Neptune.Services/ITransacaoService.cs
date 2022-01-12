using Neptune.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Neptune.Application
{
    public interface ITransacaoService
    {
        List<Transacao> ObterTodas();
        Task<Mes> ObterPorDataEContas(MesTransacao mesTransacao, int[] contasId);
        Transacao Criar(Transacao transacao);
        Transacao Atualizar(Transacao transacao);
    }
}

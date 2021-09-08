using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Neptune.Models
{
    public class Transacao //: INotifyCompletion
    {
        public byte IdView { get; set; }
        public DateTime Data { get; set; } = DateTime.Now;
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public int Id { get; set; }

        public Transacao()
        {
        }

        public Transacao(int id, byte idView, DateTime data, string descricao, decimal valor)
        {
            Id = id;
            IdView = idView;
            Data = data;
            Descricao = descricao;
            Valor = valor;
        }

        //public static TaskAwaiter GetAwaiter(this TimeSpan timeSpan)
        //{
        //    return TaskEx.Delay(timeSpan).GetAwaiter();
        //}
    }
}

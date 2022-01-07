using System.Collections.Generic;

namespace Neptune.Web.ViewModel
{
    public class Conta
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        private bool _ativo;
        public bool Ativo 
        { 
            get 
            {
                return _ativo;
            } 
            set
            {
                _ativo = value;
            }
        }

        public List<object> Interessados { get; set; } = new List<object> { };

        public Conta(int id, string nome, bool ativo)
        {
            Id = id;
            Nome = nome;
            Ativo = ativo;
        }
    }
}

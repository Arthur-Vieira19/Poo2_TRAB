using System;

namespace TrabPoo2
{
    public class RegistroTransacao
    {
        public int Id { get; set; } // Chave primária para o DB
        public DateTime DataHora { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; }

        // Relacionamento com a Conta afetada (1xN: Uma conta tem N registros)
        public Conta Conta { get; set; }
        public string ContaNumero { get; set; } // Chave estrangeira
    }
}
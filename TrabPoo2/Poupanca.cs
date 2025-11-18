using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabPoo2
{
    class Poupanca : Conta
    {
        public Poupanca(string numero, Cliente titular, string agencia) 
            : base(numero, titular, agencia, "POUPANCA")
        {

        }

        public override bool Debitar(decimal valor)
        {
            if(valor <= 0)
            {
                throw new ArgumentException("O valor do débito deve ser positivo.");
            }
            if (valor <= Saldo)
            {
                Saldo -= valor;
                return true;
            }
            return false;
        }

        public override void AplicarTaxaOuRendimento()
        {
            const decimal taxaRendimento = 0.005m; 
            decimal rendimento = Saldo * taxaRendimento;

            if (rendimento > 0)
            {
                Creditar(rendimento);

                Historico.Add(new RegistroTransacao
                {
                    DataHora = DateTime.Now,
                    Valor = rendimento,
                    Descricao = "Rendimento Mensal Poupança",
                    ContaNumero = this.Numero
                });
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabPoo2
{
    public class ContaCorrente : Conta
    {
        public decimal LimiteChequeEspecial { get; private set; }

        public ContaCorrente(string numero, Cliente titular, decimal limite, string agencia)
            : base(numero, titular,agencia, "CORRENTE")
        {
            LimiteChequeEspecial = limite;
        }

        public override bool Debitar(decimal valor)
        {
            decimal totalDisponivel = Saldo + LimiteChequeEspecial;

            if (valor <= totalDisponivel)
            {
                Saldo -= valor;
                return true;
            }
            return false;
        }

        public override void AplicarTaxaOuRendimento()
        {
            const decimal TaxaManutencao = 10.00m;
            const decimal SaldoMinimoIsencao = 500.00m;

            if (Saldo < SaldoMinimoIsencao && Debitar(TaxaManutencao)
            {
                var registro = new RegistroTransacao
                {
                    DataHora = DateTime.Now,
                    Valor = -TaxaManutencao,
                    Descricao = "Taxa de Manutenção Mensal",
                    ContaNumero = this.Numero
                };

                Historico.Add(registro);
            }
        }
    }
}

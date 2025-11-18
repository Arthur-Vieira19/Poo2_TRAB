using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabPoo2
{
    class Sacar : Transacao
    {
        private Conta _conta { get; set; }
        private decimal _valor { get; set; }

        public Sacar(Conta conta, decimal valor)
        {
            _conta = conta;
            _valor = valor;
        }

        public bool Executar()
        {
            if (_valor <= 0)
            {
                Console.WriteLine("Valor de saque inválido.");
                return false;
            }

            if (_conta.Debitar(_valor))
            {
                _conta.Historico.Add(new RegistroTransacao
                {
                    DataHora = DateTime.Now,
                    Valor = _valor,
                    Descricao = "Saque em Conta",
                    ContaNumero = _conta.Numero 
                });
                return true;
            }
            else
            {
                Console.WriteLine("Saldo insuficiente para saque.");
                return false;
            }
        }

    }
}

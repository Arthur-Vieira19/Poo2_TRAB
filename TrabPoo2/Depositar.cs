using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabPoo2
{
    class Depositar : Transacao
    {
        private Conta _conta { get; set; }
        private decimal _valor { get; set; }

        public Depositar(Conta conta, decimal valor)
        {
            _conta = conta;
            _valor = valor;
        }

        public bool Executar()
        {
            if (_valor <= 0)
            {
                Console.WriteLine("Valor de depósito inválido.");
                return false;
            }
            try
            {
                _conta.Creditar(_valor);
                _conta.Historico.Add(new RegistroTransacao
                {
                    DataHora = DateTime.Now,
                    Valor = _valor,
                    Descricao = "Depósito em Conta",
                    ContaNumero = _conta.Numero 
                });
                return true;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
    }
}

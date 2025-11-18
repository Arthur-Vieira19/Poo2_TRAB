using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabPoo2
{
    class Sacar : Transacao
    {
        private readonly Conta _conta { get; set; }
        private readonly decimal _valor { get; set; }

        public Sacar(Conta conta, decimal valor)
        {
            if(valor <= 0)
            {
                throw new ArgumentException("O valor de saque deve ser positivo");
            }
            _conta = conta;
            _valor = valor;
        }

        public bool Executar(GerenciadorDeTransacoes gerenciador)
        {
            if (_conta.Debitar(_valor))
            {
                // Delega o registro de log para o gerenciador 
                gerenciador.Registrar(new RegistroTransacao
                {
                    DataHora = DateTime.Now,
                    Valor = -_valor,
                    Descricao = "Saque em Conta",
                    ContaNumero = _conta.Numero
                }, _conta);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
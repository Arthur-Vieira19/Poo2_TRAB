using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabPoo2
{
    class Sacar : Transacao
    {
        public bool Executar()
        {
            return true;
        }
        public bool executarSaque(Cliente cliente, Conta conta, decimal valor)
        {
            if(cliente == null)
            {
                Console.WriteLine("Cliente inválido! ");
                return false;
            }
            if (cliente.Contas.Contains(conta) && conta.Saldo >= valor)
            {
                conta.Saldo -= valor;
                return Executar();
            }
            return false;
        }
    }
}

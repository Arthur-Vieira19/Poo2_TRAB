using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabPoo2.MODEL
{
   public class Poupanca : Conta
    {

        public Poupanca() : base() { }
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

        
    }
}

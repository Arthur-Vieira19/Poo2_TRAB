using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TrabPoo2.MODEL
{
    public class ContaCorrente : Conta
    {
        public decimal LimiteChequeEspecial { get; private set; }

        public ContaCorrente() : base() { }

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
    }
}

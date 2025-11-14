using System;
using System.Collections.Generic;
using TrabPoo2;

namespace TrabPoo2
{
    public abstract class Conta
    {
        public string Numero { get; protected set; }
        public decimal Saldo { get; protected set; }

        public Cliente Titular { get; set; }

        public List<RegistroTransacao> Historico { get; } = new List<RegistroTransacao>();

        public Conta(string numero, Cliente titular)
        {
            Numero = numero;
            Titular = titular;
            Saldo = 0;
        }

        public virtual void Creditar(decimal valor)
        {
            if (valor <= 0)
                throw new ArgumentException("O valor do crédito deve ser positivo.");

            Saldo += valor;
        }

        public abstract bool Debitar(decimal valor);

        public abstract void AplicarTaxaOuRendimento();
    }

}

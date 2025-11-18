using System;
using System.Collections.Generic;
using System.IO.Packaging;
using TrabPoo2;

namespace TrabPoo2
{
    public abstract class Conta
    {
        public string Tipo { get; protected set; }
        public string Numero { get; protected set; }
        public decimal Saldo { get; protected set; }
        public string Agencia { get; protected set; }


        public Cliente Titular { get; }

        public ICollection<RegistroTransacao> Historico { get; } = new List<RegistroTransacao>();

        public Conta(string numero, Cliente titular, string agencia, string tipo)
        {
            Numero = numero ?? throw new ArgumentNullException(nameof(numero));
            Titular = titular ?? throw new ArgumentNullException(nameof(titular));
            Agencia = agencia ?? throw new ArgumentNullException(nameof(agencia));
            Tipo = tipo ?? throw new ArgumentNullException(nameof(tipo));
        }

        public virtual void Creditar(decimal valor)
        {
            if (valor <= 0)
                throw new ArgumentException("O valor do crédito deve ser positivo.");

            Saldo += valor;
        }

        public abstract bool Debitar(decimal valor);

        public abstract void AplicarTaxaOuRendimento();

        public override string ToString()
        {
            return $"Conta {Tipo} - Número: {Numero}, Agência: {Agencia}, Saldo: {Saldo:C}";
        }
    }

}

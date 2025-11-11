using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabPoo2
{
    class Conta
    {
        public string Numero { get; set; }
        public string Tipo { get; set; }
        public decimal Saldo { get; set; }
        public Cliente Titular { get; set; }
        public void Depositar(decimal valor) { }
        public void Sacar(decimal valor) { }
        public void Transferir(Conta destino, decimal valor) { }
    }
}

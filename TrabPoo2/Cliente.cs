using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabPoo2
{
    class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Endereco { get; set; }
        public List<Conta> Contas { get; set; }

        public void AdicionarConta(Conta conta)
        {
            foreach (var c in Contas)
            {
                if (c.Numero == conta.Numero)
                {
                    Console.WriteLine("Conta já existe para este cliente.");
                    return;
                }
                if (c.)
            }
        }
        public void RemoverConta(Conta conta) { }
        public void ListarContas() { }
    }
}

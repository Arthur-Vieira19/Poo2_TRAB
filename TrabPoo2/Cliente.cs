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


        public Cliente(int id, string nome, string cpf, string endereco)
        {
            Id = id;
            Nome = nome;
            CPF = cpf;
            Endereco = endereco;
            Contas = new List<Conta>();
        }

        public bool AdicionarConta(Conta conta)
        {
            
                if (conta == null)
                {
                    throw new ArgumentNullException(nameof(conta), "Conta não pode ser nula.");

                }
                
                bool existe = Contas.Any(c => c.Numero == conta.Numero);
                
                if (existe)
                {
                    return false;
                }

            this.Contas.Add(conta);
                return true;
            
        }
        public bool RemoverConta(Conta conta) {
            if(conta == null)
            {
                throw new ArgumentNullException(nameof(conta), "Conta não pode ser nula.");
                
            }
            var contaEncontrada = Contas.FirstOrDefault(c => c.Numero == conta.Numero);

            if (contaEncontrada == null)
                return false;

            this.Contas.Remove(contaEncontrada);
            return true;


        }
        public void ListarContas()
        {
            foreach (var conta in Contas)
            {
                Console.WriteLine(conta);
            }
        }
    }
}

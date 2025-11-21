using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabPoo2.MODEL
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Endereco { get; set; }
        public string Senha { get; set; }

        public virtual ICollection<Conta> Contas { get; } = new List<Conta>();

        public Cliente() { }
        public Cliente(int id, string nome, string cpf, string endereco, string senha)
        {
            Id = id;
            Nome = nome ?? throw new ArgumentNullException(nameof(nome));
            CPF = cpf ?? throw new ArgumentNullException(nameof(cpf));
            Endereco = endereco ?? throw new ArgumentNullException(nameof(endereco));
            Senha = senha ?? throw new ArgumentNullException(nameof(senha));
        }

        public bool AdicionarConta(Conta conta)
        {
            if (conta == null)
            {
                throw new ArgumentNullException(nameof(conta), "Conta não pode ser nula.");
            }

            if (Contas.Any(c => c.Numero == conta.Numero))
            {
                return false;
            }

            Contas.Add(conta);
            return true;
        }
        public bool RemoverConta(Conta conta)
        {
            if (conta == null)
            {
                throw new ArgumentNullException(nameof(conta), "Conta não pode ser nula.");

            }
            var contaEncontrada = Contas.FirstOrDefault(c => c.Numero == conta.Numero);

            if (contaEncontrada == null)
                return false;

            Contas.Remove(contaEncontrada);
            return true;


        }
        /* public IReadOnlyList<Cliente> ListarClientes()
         {
             return clientes.AsReadOnly();
         }*/
    }
}
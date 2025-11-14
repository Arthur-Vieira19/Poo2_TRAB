using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabPoo2
{
    class GerenciadorDeClientes
    {
        private List<Cliente> clientes;
        public GerenciadorDeClientes() {
            clientes = new List<Cliente>();
        }
        public void Adicionar(Cliente cliente) {
            foreach (Cliente c in clientes) {
                if (c.Id == cliente.Id) {
                    Console.WriteLine("CLIENTE JA CADASTRADO!");
                
                }
            
            }
            clientes.Add(cliente);

        }

        public void Remover(Cliente cliente) {

            clientes.RemoveAll(c => c.Id == cliente.Id);

        }

        public Cliente BuscarPorCPF(string cpf) {
            foreach (Cliente c1 in clientes) {
                if (c1.CPF == cpf)
                {

                    Console.WriteLine($"O CPF: {cpf}, foi encontrado!");
                    
                }

            }




            Console.WriteLine($"CPF {cpf}, nao encontrado");

            return null;
        }
        public Cliente BuscarPorId(int id) {
            foreach (Cliente c in clientes)
            { if (c.Id == id)
                {
                    Console.WriteLine("CLIENTE ENCONTRADO!");

                }
            }

            Console.WriteLine("CLIENTE INEXISTENTE!");
            return null;


        }

        public List<Cliente> ListarClientes() {

            foreach (Cliente c in clientes) {
                Console.WriteLine(c);
            }
            return null;
        }
    } 
}

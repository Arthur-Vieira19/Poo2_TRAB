using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabPoo2
{
    class GerenciadorDeClientes
    {
        private readonly List<Cliente> clientes;

        public GerenciadorDeClientes() {
            clientes = new List<Cliente>();
        }

        public bool Adicionar(Cliente cliente) {
            if (clientes.Any(c => c.Id == cliente.Id))
            {
                return false;
            }

            clientes.Add(cliente);
            return true;
        }

        public void Remover(Cliente cliente) {

            clientes.RemoveAll(c => c.Id == cliente.Id);

        }

        public Cliente BuscarPorCPF(string cpf) {
            return clientes.FirstOrDefault(c => c.CPF == cpf);
        }

        public Cliente BuscarPorId(int id) {
            return clientes.FirstOrDefault(c => c.Id == id);
        }

        public List<Cliente> ListarClientes() {
            return clientes;
        }
    } 
}

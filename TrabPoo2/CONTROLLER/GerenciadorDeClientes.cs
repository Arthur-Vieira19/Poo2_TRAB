using System;
using System.Collections.Generic;
using System.Linq;
using TrabPoo2.MODEL;

namespace TrabPoo2.CONTROLLER
{
    public class GerenciadorDeClientes
    {
        private readonly AppDbContext _context;

        // Construtor agora recebe o contexto do banco
        public GerenciadorDeClientes(AppDbContext context)
        {
            _context = context;
        }

        public bool Adicionar(Cliente cliente)
        {
            // Verifica no Banco se já existe esse ID
            if (_context.Clientes.Any(c => c.Id == cliente.Id))
            {
                return false;
            }

            _context.Clientes.Add(cliente);
            _context.SaveChanges(); // Persiste no SQL
            return true;
        }

        public void Remover(Cliente cliente)
        {
            // Busca a entidade rastreada pelo EF para remover
            var clienteNoBanco = _context.Clientes.Find(cliente.Id);
            if (clienteNoBanco != null)
            {
                _context.Clientes.Remove(clienteNoBanco);
                _context.SaveChanges();
            }
        }

        public Cliente BuscarPorCPF(string cpf)
        {
            return _context.Clientes.FirstOrDefault(c => c.CPF == cpf);
        }

        public Cliente BuscarPorId(int id)
        {
            return _context.Clientes.Find(id);
        }

        public List<Cliente> ListarClientes()
        {
            return _context.Clientes.ToList();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabPoo2.CONTROLLER;

namespace TrabPoo2.MODEL
{
    class Banco
    {
        public string Nome { get; }
        public string Codigo { get; }

        private AppDbContext _context;

        public GerenciadorDeContas GerContas { get; }
        public GerenciadorDeClientes GerClientes { get; }
        public GerenciadorDeTransacoes GerTransacoes { get; }
        public Calendario Calendario { get; }

        public Banco(string nome, string codigo) {
            Nome = nome;
            Codigo = codigo;

            _context = new AppDbContext();
            _context.Database.EnsureCreated();

            GerClientes = new GerenciadorDeClientes(_context);
            GerContas = new GerenciadorDeContas(_context);
            GerTransacoes = new GerenciadorDeTransacoes(_context);

            Calendario = new Calendario();
        }

        public bool ExecutarTransacao(Transacao transacao)
        {
            if (transacao == null)
            {
                throw new ArgumentNullException(nameof(transacao));
            }
            // Chama a execução, passando o GerenciadorDeTransacoes como dependência
            return transacao.Executar(GerTransacoes);
        }

        /*
        public void adicionarCliente(Cliente cliente) {
            if (cliente == null) {
                Console.WriteLine("CLIENTE INEXISTENTE!");
            }
            GerClientes.Adicionar(cliente);
        }
        public void removerCliente(Cliente cliente) {
            if (cliente == null) {
                Console.WriteLine("CLIENTE INEXISTENTE");
            }
            else { 
                GerClientes.Remover(cliente);
            }
        }
        */
    }
}

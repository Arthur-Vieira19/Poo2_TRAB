using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabPoo2
{
    class Banco
    {
        public string Nome { get; }
        public string Codigo { get; }

        public GerenciadorDeContas GerContas { get; }
        public GerenciadorDeClientes GerClientes { get; }
        public GerenciadorDeTransacoes GerTransacoes { get; }
        public Calendario Calendario { get; }

        public Banco(string nome, string codigo) {
            Nome = nome;
            Codigo = codigo;

            GerContas = new GerenciadorDeContas();
            GerClientes = new GerenciadorDeClientes();
            GerTransacoes = new GerenciadorDeTransacoes();
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabPoo2
{
        class Banco
        {
                public string Nome { get; set; }
                public string Codigo { get; set; }

                public GerenciadorDeContas GerContas { get; set; }
                public GerenciadorDeClientes GerClientes { get; set; }
                public GerenciadorDeTransacoes GerTransacoes { get; set; }
                public Calendario Calendario { get; set; }
            public Banco(string nome, string codigo) {
            Nome = nome;
            Codigo = codigo;



            GerContas = new GerenciadorDeContas();
            GerClientes = new GerenciadorDeClientes();
            GerTransacoes = new GerenciadorDeTransacoes();
        
        
        }


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
        }
    }

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

                public GerenciadorDeContas gerContas { get; set; }
                public GerenciadorDeClientes gerClientes { get; set; }
                public GerenciadorDeTransacoes gerTransacoes { get; set; }
                public Calendario Calendario { get; set; }


                public void adicionarCliente(Cliente cliente) { }
                public void removerCliente(Cliente cliente) { }
        }
    }

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabPoo2.CONTROLLER;

namespace TrabPoo2.MODEL
{
    public interface Transacao
    {
        bool Executar(GerenciadorDeTransacoes gerenciador);
    }
}

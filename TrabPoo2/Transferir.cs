using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//parei aqui
namespace TrabPoo2
{
    public class Transferir : Transacao
    {
        private Conta _contaOriginal;
        private Conta _contaDestino;
        private decimal _valor;

        // Construtor para inicializar os dados
        public Transferir(Conta contaOriginal, Conta contaDestino, decimal valor)
        {
            if (valor <= 0)
                throw new ArgumentException("O valor da transferência deve ser positivo.");

            _contaOriginal = contaOriginal;
            _contaDestino = contaDestino;
            _valor = valor;
        }

        public bool Executar(GerenciadorDeTransacoes gerenciador)
        {
            if (gerenciador == null)
                throw new ArgumentNullException(nameof(gerenciador));

            if (_contaOriginal.Debitar(_valor))
            {
                _contaDestino.Creditar(_valor);

                gerenciador.Registrar(new RegistroTransacao
                {
                    DataHora = DateTime.Now,
                    Valor = -_valor, 
                    Descricao = $"Transferência Enviada p/ Conta {_contaDestino.Numero}",
                }, _contaOriginal);

                gerenciador.Registrar(new RegistroTransacao
                {
                    DataHora = DateTime.Now,
                    Valor = _valor, 
                    Descricao = $"Transferência Recebida da Conta {_contaOriginal.Numero}",
                }, _contaDestino);

                return true;
            }
            else
            {
                return false;
            }
        }

    }
}


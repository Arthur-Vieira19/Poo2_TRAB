using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        // Método Executar() da interface Transacao
        public bool Executar()
        {
            // 1. Tenta debitar da conta original
            if (_contaOriginal.Debitar(_valor))
            {
                // 2. Se o débito for bem-sucedido, credita a conta destino
                _contaDestino.Creditar(_valor);

                // 3. Registra a transação nas duas contas
                RegistrarTransacao("Transferência Enviada", -_valor, _contaOriginal); // Débito é negativo
                RegistrarTransacao("Transferência Recebida", _valor, _contaDestino); // Crédito é positivo

                return true;
            }
            else
            {
                // O método Debitar() da conta já lida com saldo insuficiente.
                return false;
            }
        }

        // Método auxiliar (Clean Code)
        private void RegistrarTransacao(string descricao, decimal valor, Conta conta)
        {
            var registro = new RegistroTransacao
            {
                DataHora = DateTime.Now,
                Valor = valor,
                Descricao = descricao,
                Conta = conta, // Se for usar EF Core, este pode ser opcional se ContaNumero for a chave
                ContaNumero = conta.Numero 
            };
            conta.Historico.Add(registro);
        }
    }
}


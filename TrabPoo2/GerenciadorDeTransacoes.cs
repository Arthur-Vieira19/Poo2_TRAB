using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabPoo2
{
    public class GerenciadorDeTransacoes
    {
        private readonly List<RegistroTransacao> _transacoes;

        public GerenciadorDeTransacoes()
        {
            _transacoes = new List<RegistroTransacao>();
        }

        // Registrar uma transação
        public void Registrar(RegistroTransacao registro)
        {
            if (registro == null)
                throw new ArgumentNullException(nameof(registro));

            _transacoes.Add(registro);
        }

        // Obter todas as transações
        public List<RegistroTransacao> ObterTodas()
        {
            return new List<RegistroTransacao>(_transacoes);
        }

        // Filtrar por conta
        public List<RegistroTransacao> ObterPorConta(string numeroConta)
        {
            return _transacoes
                .Where(t => t.ContaNumero == numeroConta)
                .ToList();
        }

        // Filtrar por período
        public List<RegistroTransacao> ObterPorPeriodo(DateTime inicio, DateTime fim)
        {
            return _transacoes
                .Where(t => t.DataHora >= inicio && t.DataHora <= fim)
                .ToList();
        }

        // Total movimentado em uma conta
        public decimal TotalMovimentado(string numeroConta)
        {
            return _transacoes
                .Where(t => t.ContaNumero == numeroConta)
                .Sum(t => t.Valor);
        }

        // Log simples para depuração
        public void ExibirLog()
        {
            foreach (var t in _transacoes)
            {
                Console.WriteLine($"{t.DataHora} | {t.ContaNumero} | {t.Descricao} | {t.Valor}");
            }
        }
    }
}

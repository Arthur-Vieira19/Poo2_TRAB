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
        public void Registrar(RegistroTransacao registro, Conta conta)
        {
            if (registro == null)
                throw new ArgumentNullException(nameof(registro));
            if (conta == null)
                throw new ArgumentException("A transação deve estar associada a uma conta.");

            registro.Conta = conta;
            registro.ContaNumero = conta.Numero;

            _transacoes.Add(registro);
            conta.Historico.Add(registro);
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
            DateTime fimExclusivo = fim.Date.AddDays(1);

            return _transacoes
                .Where(t => t.DataHora >= inicio.Date && t.DataHora <= fimExclusivo)
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
            Console.WriteLine("--- LOG DE TRANSAÇÕES ---");
            foreach (var t in _transacoes)
            {
                // Formata o valor como moeda e a data/hora de forma mais legível
                Console.WriteLine($"{t.DataHora:dd/MM/yyyy HH:mm:ss} | Conta: {t.ContaNumero} | {t.Descricao} | Valor: {t.Valor.ToString("C")}");
            }
            Console.WriteLine("-------------------------");
        }
    }
}

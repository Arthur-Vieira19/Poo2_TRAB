using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabPoo2.MODEL;

namespace TrabPoo2.CONTROLLER
{
    public class GerenciadorDeTransacoes
    {
        private readonly AppDbContext _context;

        public GerenciadorDeTransacoes(AppDbContext context)
        {
            _context = context;
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

            _context.TransacoesRegistradas.Add(registro);

            _context.Contas.Update(conta);

            _context.SaveChanges();
        }

        // Obter todas as transações
        public List<RegistroTransacao> ObterTodas()
        {
            return _context.TransacoesRegistradas.ToList();
        }

        // Filtrar por conta
        public List<RegistroTransacao> ObterPorConta(string numeroConta)
        {
            return _context.TransacoesRegistradas
                .Where(t => t.ContaNumero == numeroConta)
                .OrderByDescending(t => t.DataHora)
                .ToList();
        }

        // Filtrar por período
        public List<RegistroTransacao> ObterPorPeriodo(DateTime inicio, DateTime fim)
        {
            DateTime fimExclusivo = fim.Date.AddDays(1);

            return _context.TransacoesRegistradas
                .Where(t => t.DataHora >= inicio && t.DataHora < fimExclusivo)
                .ToList();
        }

        // Total movimentado em uma conta
        public decimal TotalMovimentado(string numeroConta)
        {
            return _context.TransacoesRegistradas
                .Where(t => t.ContaNumero == numeroConta)
                .Sum(t => t.Valor);
        }

    }
}

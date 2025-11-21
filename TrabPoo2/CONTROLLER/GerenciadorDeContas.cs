using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TrabPoo2.MODEL;

namespace TrabPoo2.CONTROLLER
{
    public  class GerenciadorDeContas
    {
        private readonly AppDbContext _context;

        public GerenciadorDeContas(AppDbContext context)
        {
            _context = context;
        }

        public Conta? CriarConta(string agencia, string numero, Cliente titular, string tipo)
        {
            if (_context.Contas.Any(c => c.Numero == numero && c.Agencia == agencia))
            {
                return null; // Conta com o mesmo número e agência já existe
            }

            Conta? novaConta = tipo.ToUpper() switch
            {
                "CORRENTE" => new ContaCorrente(numero, titular, 0, agencia),
                "POUPANCA" => new Poupanca(numero, titular, agencia), // Assumindo saldo inicial zero no construtor de Poupanca
                _ => null // Default (caso o tipo seja inválido)
            };

            if (novaConta != null)
            {
                _context.Contas.Add(novaConta);
                _context.SaveChanges();
            }
       
            return novaConta;
        }

        public bool FecharConta(Conta conta) {
            var contaExistente = _context.Contas.FirstOrDefault(c => c.Numero == conta.Numero);
            
            if (contaExistente != null)
            {
                _context.Contas.Remove(contaExistente);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public Conta? BuscarPorNumero(string agencia, string numero)
        {
            return _context.Contas.FirstOrDefault(c => c.Numero == numero && c.Agencia == agencia);
        }

        public List<Conta> BuscarPorCliente(Cliente cliente)
        {
            return _context.Contas.Where(c => c.TitularId == cliente.Id).ToList();
        }

        public List<Conta> ListarContas()
        {
            return _context.Contas.ToList();
        }

        public decimal ObterPatrimonioTotalViaSQL()
        {
            decimal total = 0;

            var connection = _context.Database.GetDbConnection();

            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT SUM(Saldo) FROM Contas";

                    var result = command.ExecuteScalar();

                    // Tratamento caso o banco esteja vazio (DBNull)
                    if (result != DBNull.Value && result != null)
                    {
                        total = Convert.ToDecimal(result);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro no SQL Direto: " + ex.Message);
                throw;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return total;
        }
    }

}

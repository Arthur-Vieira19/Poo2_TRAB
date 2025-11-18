using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabPoo2
{
    class GerenciadorDeContas
    {
        private readonly List<Conta> contas;

        public GerenciadorDeContas()
        {
            contas = new List<Conta>();
        }

        public Conta CriarConta(string agencia, string numero, Cliente titular, string tipo)
        {
            if (contas.Any(c => c.Numero == numero && c.Agencia == agencia))
            {
                // Melhor prática: Retornar null e deixar o chamador exibir a mensagem de erro.
                return null;
            }

            Conta novaConta = tipo.ToUpper() switch
            {
                "CORRENTE" => new ContaCorrente(numero, titular, 0, agencia),
                "POUPANCA" => new Poupanca(numero, titular, agencia), // Assumindo saldo inicial zero no construtor de Poupanca
                _ => null // Default (caso o tipo seja inválido)
            };

            if (novaConta != null)
            {
                contas.Add(novaConta);
            }
       
            return novaConta;
        }

        public bool FecharConta(Conta conta) {
            return contas.Remove(conta);
        }

        public Conta BuscarPorNumero(string agencia, string numero)
        {
            return contas.FirstOrDefault(c => c.Numero == numero && c.Agencia == agencia);
        }

        public List<Conta> BuscarPorCliente(Cliente cliente)
        {
            return contas.Where(c => c.Titular.Id == cliente.Id).ToList();
        }

        public List<Conta> ListarContas()
        {
            foreach (Conta c in contas) {

                Console.WriteLine(c);
            }
            return contas;
        }
    }

}

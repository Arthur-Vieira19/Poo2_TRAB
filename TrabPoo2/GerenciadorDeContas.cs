using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabPoo2
{
    class GerenciadorDeContas
    {
        private List<Conta> contas;

        public GerenciadorDeContas()
        {
            contas = new List<Conta>();
        }

        public Conta CriarConta(string agencia, string numero, Cliente titular, string tipo)
        {
            // Verifica duplicidade
            foreach (Conta c in contas)
            {
                if (c.Numero == numero && c.Agencia == agencia)
                {
                    Console.WriteLine("CONTA JA EXISTENTE!");
                    return null;
                }
            }

            Conta novaConta = null;

            if (tipo.ToUpper() == "CORRENTE")
                novaConta = new ContaCorrente(numero,titular,0,agencia);

            else if (tipo.ToUpper() == "POUPANCA")
                novaConta = new Poupanca(numero, titular, agencia);

            else
            {
                Console.WriteLine("TIPO DE CONTA INVALIDO!");
                return null;
            }

            contas.Add(novaConta);

            Console.WriteLine("CONTA CRIADA COM SUCESSO!");
            return novaConta;
        }

        public void FecharConta(Conta conta) {
            foreach (Conta c in contas) {
                if (c.Numero == conta.Numero) {
                    contas.Remove(conta);
                }
                else { Console.WriteLine("CONTA INEXISTENTE"); }
            }
        
        }

        public Conta BuscarPorNumero(string agencia, string numero)
        {
            foreach (Conta c in contas)
            {
                if (c.Numero == numero)
                {
                    Console.WriteLine("CONTA ENCONTRADA!");
                }
            }   
            return null;
        }

        public List<Conta> BuscarPorCliente(Cliente cliente)
        {
            List<Conta> resultado = new List<Conta>();

            foreach (Conta c in contas)
            {
                if (c.Titular.Id == cliente.Id)
                    resultado.Add(c);
            }

            return resultado;
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

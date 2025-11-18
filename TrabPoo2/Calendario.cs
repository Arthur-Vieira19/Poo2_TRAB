using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabPoo2
{
    class Calendario
    {
        public DateTime DataAtual { get; private set; } = DateTime.Today;

        public void AvancarDia() {
            DataAtual = DataAtual.AddDays(1);
        }
        public void RetrocederDia()
        {
            DataAtual = DataAtual.AddDays(-1);
        }
        public void ExibirDataAtual()
        {
            Console.WriteLine("Data:\n" + DataAtual.ToShortDateString());
        }
    }
}

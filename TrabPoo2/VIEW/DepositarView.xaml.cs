using System.Windows;
using TrabPoo2.MODEL;

namespace TrabPoo2.Views
{
    public partial class DepositarView : Window
    {
        private Cliente _cliente;

        public DepositarView(Cliente cliente)
        {
            InitializeComponent();
            _cliente = cliente;
            CarregarContas();
        }

        private void CarregarContas()
        {
            var contas = MainWindow.BancoSistema.GerContas.BuscarPorCliente(_cliente);
            cbContas.ItemsSource = contas;
            // DisplayMemberPath no XAML vai mostrar apenas o Numero, 
            // mas você pode sobrescrever o ToString() na classe Conta para ficar mais bonito ex: "Corrente - 12345"
        }

        private void BtnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            if (cbContas.SelectedItem is Conta contaSelecionada && decimal.TryParse(txtValor.Text, out decimal valor))
            {
                // 1. Cria a transação (Polimorfismo em ação!)
                Transacao deposito = new Depositar(contaSelecionada, valor);

                // 2. Executa através do Banco (MVC)
                bool sucesso = MainWindow.BancoSistema.ExecutarTransacao(deposito);

                if (sucesso)
                {
                    MessageBox.Show("Depósito realizado com sucesso!");
                    VoltarProDashboard();
                }
                else
                {
                    MessageBox.Show("Erro ao depositar. Verifique o valor.");
                }
            }
            else
            {
                MessageBox.Show("Selecione uma conta e digite um valor válido.");
            }
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e) => VoltarProDashboard();

        private void VoltarProDashboard()
        {
            new DashboardView(_cliente).Show();
            this.Close();
        }
    }
}
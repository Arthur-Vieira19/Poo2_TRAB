using System.Windows;
using TrabPoo2.MODEL;

namespace TrabPoo2.Views
{
    public partial class SacarView : Window
    {
        private Cliente _cliente;

        public SacarView(Cliente cliente)
        {
            InitializeComponent();
            _cliente = cliente;
            CarregarContas();
        }

        private void CarregarContas()
        {
            var contas = MainWindow.BancoSistema.GerContas.BuscarPorCliente(_cliente);
            cbContas.ItemsSource = contas;
        }

        private void BtnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            if (cbContas.SelectedItem is Conta contaSelecionada && decimal.TryParse(txtValor.Text, out decimal valor))
            {
                if (valor <= 0)
                {
                    MessageBox.Show("O valor deve ser positivo.");
                    return;
                }

                // Instancia a transação de Saque
                Transacao saque = new Sacar(contaSelecionada, valor);

                // Executa
                bool sucesso = MainWindow.BancoSistema.ExecutarTransacao(saque);

                if (sucesso)
                {
                    MessageBox.Show("Saque realizado com sucesso!", "Sucesso");
                    Voltar();
                }
                else
                {
                    MessageBox.Show("Saldo insuficiente ou erro na operação.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Selecione uma conta e informe um valor válido.");
            }
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Voltar();
        }

        private void Voltar()
        {
            DashboardView dashboard = new DashboardView(_cliente);
            dashboard.Show();
            this.Close();
        }
    }
}
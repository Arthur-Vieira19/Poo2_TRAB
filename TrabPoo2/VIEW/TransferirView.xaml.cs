using System.Windows;
using TrabPoo2.MODEL;

namespace TrabPoo2.Views
{
    public partial class TransferirView : Window
    {
        private Cliente _cliente;

        public TransferirView(Cliente cliente)
        {
            InitializeComponent();
            _cliente = cliente;
            CarregarContasOrigem();
        }

        private void CarregarContasOrigem()
        {
            var contas = MainWindow.BancoSistema.GerContas.BuscarPorCliente(_cliente);
            cbContaOrigem.ItemsSource = contas;
        }

        private void BtnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            if (cbContaOrigem.SelectedItem is Conta contaOrigem && decimal.TryParse(txtValor.Text, out decimal valor))
            {
                string agenciaDest = txtAgenciaDestino.Text;
                string numDest = txtNumeroDestino.Text;

                // 1. Validações básicas
                if (string.IsNullOrEmpty(agenciaDest) || string.IsNullOrEmpty(numDest))
                {
                    MessageBox.Show("Preencha os dados da conta de destino.");
                    return;
                }

                if (valor <= 0)
                {
                    MessageBox.Show("O valor deve ser positivo.");
                    return;
                }

                // 2. Busca Conta Destino
                Conta contaDestino = MainWindow.BancoSistema.GerContas.BuscarPorNumero(agenciaDest, numDest);

                if (contaDestino == null)
                {
                    MessageBox.Show("Conta de destino não encontrada no sistema.");
                    return;
                }

                if (contaOrigem.Numero == contaDestino.Numero)
                {
                    MessageBox.Show("Não é possível transferir para a mesma conta.");
                    return;
                }

                // 3. Executa Transferência
                Transacao transf = new Transferir(contaOrigem, contaDestino, valor);
                bool sucesso = MainWindow.BancoSistema.ExecutarTransacao(transf);

                if (sucesso)
                {
                    MessageBox.Show($"Transferência de {valor:C} realizada com sucesso!");
                    Voltar();
                }
                else
                {
                    MessageBox.Show("Saldo insuficiente na conta de origem.");
                }
            }
            else
            {
                MessageBox.Show("Selecione sua conta e um valor válido.");
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
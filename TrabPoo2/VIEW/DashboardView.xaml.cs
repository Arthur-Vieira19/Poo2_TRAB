using System.Linq;
using System.Windows;
using TrabPoo2.MODEL; // Importante

namespace TrabPoo2.Views
{
    public partial class DashboardView : Window
    {
        private Cliente _clienteLogado;

        public DashboardView(Cliente cliente)
        {
            InitializeComponent();
            _clienteLogado = cliente;
            CarregarDados();
        }

        private void CarregarDados()
        {
            txtBemVindo.Text = $"Olá, {_clienteLogado.Nome}";

            // 1. Buscar contas atualizadas do banco
            var contas = MainWindow.BancoSistema.GerContas.BuscarPorCliente(_clienteLogado);

            var cc = contas.OfType<ContaCorrente>().FirstOrDefault();
            var cp = contas.OfType<Poupanca>().FirstOrDefault();

            // 2. Atualizar Interface
            if (cc != null)
            {
                txtSaldoCC.Text = cc.Saldo.ToString("C2");
                txtNumCC.Text = $"Ag: {cc.Agencia} | CC: {cc.Numero}";
            }
            else
            {
                txtSaldoCC.Text = "Não possui";
                txtNumCC.Text = "-";
            }

            if (cp != null)
            {
                txtSaldoCP.Text = cp.Saldo.ToString("C2");
                txtNumCP.Text = $"Ag: {cp.Agencia} | CP: {cp.Numero}";
            }
            else
            {
                txtSaldoCP.Text = "Não possui";
                txtNumCP.Text = "-";
            }

            // 3. Carregar últimas transações (Requisito visual)
            // Pega todas as transações de todas as contas do cliente
            var todasTransacoes = MainWindow.BancoSistema.GerTransacoes.ObterTodas()
                .Where(t => contas.Any(c => c.Numero == t.ContaNumero))
                .OrderByDescending(t => t.DataHora)
                .Take(10) // Apenas as 10 últimas
                .ToList();

            gridTransacoes.ItemsSource = todasTransacoes;
        }

        // Navegação
        private void BtnDepositar_Click(object sender, RoutedEventArgs e) => AbrirJanela(new DepositarView(_clienteLogado));
        private void BtnSacar_Click(object sender, RoutedEventArgs e) => AbrirJanela(new SacarView(_clienteLogado)); // Você precisará criar essa View similar à Depositar
        private void BtnTransferir_Click(object sender, RoutedEventArgs e) => AbrirJanela(new TransferirView(_clienteLogado));
        private void BtnExtrato_Click(object sender, RoutedEventArgs e) => AbrirJanela(new ExtratoView(_clienteLogado));

        private void BtnNovaConta_Click(object sender, RoutedEventArgs e)
        {
            // Cria a janela passando o cliente atual
            CriarContaView telaCriacao = new CriarContaView(_clienteLogado);

            // Abre a janela como MODAL (o código para aqui até a janela fechar)
            telaCriacao.ShowDialog();

            // Verifica se a conta foi criada
            if (telaCriacao.ContaCriadaComSucesso)
            {
                // Recarrega os dados da tela para mostrar a nova conta e saldo
                CarregarDados();
            }
        }

        private void BtnSair_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void AbrirJanela(Window janela)
        {
            janela.Show();
            this.Close(); // Fecha o Dashboard ao navegar (ou use .ShowDialog() para manter aberto)
        }

        private void BtnPatrimonio_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Chama o método SQL Puro que criamos
                decimal total = MainWindow.BancoSistema.GerContas.ObterPatrimonioTotalViaSQL();

                MessageBox.Show($"Patrimônio Total Administrado pelo Banco:\n\n{total:C2}",
                                "Relatório SQL (Requisito)",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao consultar SQL: {ex.Message}", "Erro");
            }
        }
    }
}
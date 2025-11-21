using System.Windows;
using System.Linq;
using System.Collections.Generic;
using TrabPoo2.MODEL;

namespace TrabPoo2.Views
{
    public partial class ExtratoView : Window
    {
        private Cliente _cliente;

        public ExtratoView(Cliente cliente)
        {
            InitializeComponent();
            _cliente = cliente;
            CarregarExtratoInicial();
        }

        private void CarregarExtratoInicial()
        {
            // Ao abrir, mostra tudo (ou limite a 30 dias se preferir)
            var transacoes = MainWindow.BancoSistema.GerTransacoes.ObterTodas();
            FiltrarExibir(transacoes);
        }

        private void BtnFiltrar_Click(object sender, RoutedEventArgs e)
        {
            if (dtInicio.SelectedDate.HasValue && dtFim.SelectedDate.HasValue)
            {
                var filtradas = MainWindow.BancoSistema.GerTransacoes.ObterPorPeriodo(
                    dtInicio.SelectedDate.Value,
                    dtFim.SelectedDate.Value
                );
                FiltrarExibir(filtradas);
            }
            else
            {
                MessageBox.Show("Selecione as datas de início e fim.");
            }
        }

        private void FiltrarExibir(List<RegistroTransacao> transacoesBrutas)
        {
            // O banco traz transações de TODOS os clientes.
            // Precisamos filtrar apenas as contas do cliente logado.
            var minhasContas = MainWindow.BancoSistema.GerContas.BuscarPorCliente(_cliente);

            var meusExtratos = transacoesBrutas
                .Where(t => minhasContas.Any(c => c.Numero == t.ContaNumero))
                .OrderByDescending(t => t.DataHora)
                .ToList();

            gridExtrato.ItemsSource = meusExtratos;
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            DashboardView dashboard = new DashboardView(_cliente);
            dashboard.Show();
            this.Close();
        }
    }
}
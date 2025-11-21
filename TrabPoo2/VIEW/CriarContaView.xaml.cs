using System;
using System.Windows;
using System.Windows.Controls;
using TrabPoo2.MODEL;

namespace TrabPoo2.Views
{
    public partial class CriarContaView : Window
    {
        private Cliente _cliente;

        // Propriedade para indicar ao Dashboard se a criação foi bem sucedida
        public bool ContaCriadaComSucesso { get; private set; } = false;

        public CriarContaView(Cliente cliente)
        {
            InitializeComponent();
            _cliente = cliente;
        }

        private void BtnCriar_Click(object sender, RoutedEventArgs e)
        {
            // 1. Definir Agência Padrão e Gerar Número Aleatório
            string agencia = "001";
            Random rnd = new Random();
            string numeroConta = rnd.Next(10000, 99999).ToString(); // Gera ex: "45291"

            // 2. Pegar o Tipo Selecionado
            string tipo = "CORRENTE"; // Padrão
            if (cbTipoConta.SelectedItem is ComboBoxItem item && item.Content.ToString() == "Conta Poupança")
            {
                tipo = "POUPANCA";
            }

            // 3. Verificar se o cliente já tem esse tipo de conta (Regra de Negócio Opcional)
            // Aqui, vamos permitir múltiplas, mas se quiser limitar a uma de cada, faça a verificação aqui.

            try
            {
                // 4. Chamar o Gerenciador
                // Nota: O método CriarConta do seu gerenciador retorna null se falhar ou a conta se der certo
                Conta? novaConta = MainWindow.BancoSistema.GerContas.CriarConta(agencia, numeroConta, _cliente, tipo);

                if (novaConta != null)
                {
                    MessageBox.Show($"Conta {tipo} criada com sucesso!\nAgência: {agencia}\nConta: {numeroConta}", "Sucesso");
                    ContaCriadaComSucesso = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Erro ao criar conta. Tente novamente (Conflito de numeração).", "Erro");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}", "Erro Crítico");
            }
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
using System.Windows;
using TrabPoo2.MODEL; // Necessário para acessar a classe Cliente

namespace TrabPoo2.Views
{
    public partial class CadastroClienteView : Window
    {
        public CadastroClienteView()
        {
            InitializeComponent();
        }

        private void BtnCriarConta_Click(object sender, RoutedEventArgs e)
        {
            // 1. Obter valores dos campos
            string nome = txtNome.Text;
            string cpf = txtCPF.Text;
            string endereco = txtEndereco.Text;
            string senha = txtSenha.Password;
            string confirmar = txtConfirmarSenha.Password;

            // 2. Validações Básicas
            if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(cpf) || string.IsNullOrWhiteSpace(senha))
            {
                MessageBox.Show("Por favor, preencha todos os campos obrigatórios.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (senha != confirmar)
            {
                MessageBox.Show("As senhas não coincidem.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // 3. Tenta criar o cliente no Banco de Dados
            try
            {
                // Cria o objeto Cliente (Ajuste o construtor conforme sua classe Cliente.cs atualizada)
                Cliente novoCliente = new Cliente
                {
                    Nome = nome,
                    CPF = cpf,
                    Endereco = endereco,
                    Senha = senha
                };

                // Usa a instância global do Banco para salvar
                bool sucesso = MainWindow.BancoSistema.GerClientes.Adicionar(novoCliente);

                if (sucesso)
                {
                    MessageBox.Show("Conta criada com sucesso!", "Bem-vindo");

                    // Volta para o Login automaticamente
                    MainWindow login = new MainWindow();
                    login.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Já existe um cliente com este ID ou CPF.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar no banco de dados: {ex.Message}", "Erro Crítico");
            }
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            // Volta para a tela de Login
            MainWindow login = new MainWindow();
            login.Show();
            this.Close();
        }
    }
}
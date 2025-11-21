using System.Windows;
using System.Windows.Input;
using TrabPoo2.MODEL; // Certifique-se de importar

namespace TrabPoo2.Views
{
    public partial class MainWindow : Window
    {
        // Instância estática do Banco para ser acessada por todo o app (Simplificação para o trabalho)
        // Em projetos reais, usaríamos Injeção de Dependência mais robusta.
        internal static Banco BancoSistema { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            // Inicializa o Banco (Conecta no DB)
            if (BancoSistema == null)
                BancoSistema = new Banco("UVV Bank", "001");
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string cpf = txtCPF.Text;
            string senha = txtSenha.Password;

            // 1. Busca o cliente no banco
            var cliente = BancoSistema.GerClientes.BuscarPorCPF(cpf);

            // 2. Valida
            if (cliente != null && cliente.Senha == senha)
            {
                // SUCESSO: Vai para o Dashboard (que criaremos depois)
                MessageBox.Show($"Bem-vindo, {cliente.Nome}!", "Sucesso");

                DashboardView dashboard = new DashboardView(cliente);
                dashboard.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("CPF ou Senha inválidos.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TxtCadastro_Click(object sender, MouseButtonEventArgs e)
        {
            // Abre a tela de cadastro
            CadastroClienteView cadastro = new CadastroClienteView();
            cadastro.Show();
            this.Close();
        }
    }
}
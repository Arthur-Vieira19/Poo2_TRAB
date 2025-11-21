// Arquivo: TrabPoo2.MODEL/AppDbContext.cs (CÓDIGO FINAL)

using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore.Proxies;

namespace TrabPoo2.MODEL // Certifique-se de que este é o namespace correto
{
    public class AppDbContext : DbContext
    {
        // Construtores para uso do código de negócio e ferramentas (Migrations)
        public AppDbContext() { }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // --- APENAS CLASSES DE DOMÍNIO ---
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Conta> Contas { get; set; }
        public DbSet<RegistroTransacao> TransacoesRegistradas { get; set; }

        // REMOVIDOS: GerenciadoresDeContas, GerenciadoresDeClientes, ContasCorrente, Poupancas.

        // 1. Configuração da Conexão (SQL Server)
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // String de Conexão para SQL Server LocalDB (Versão padrão do Visual Studio)
                string connectionString = "Server=(localdb)\\mssqllocaldb;Database=UVVFintechDB;Trusted_Connection=True;MultipleActiveResultSets=true";

                optionsBuilder.UseSqlServer(connectionString);

                // Habilita o Carregamento Lento
                optionsBuilder.UseLazyLoadingProxies();
            }
            base.OnConfiguring(optionsBuilder);
        }

        // 2. Mapeamento de Modelos e Relacionamentos (Fluent API)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // --- A. CONFIGURAÇÃO DE PRECISÃO DECIMAL (RESOLVE O AVISO DE TRUNCAMENTO) ---

            // Saldo na Conta
            modelBuilder.Entity<Conta>()
                .Property(c => c.Saldo)
                .HasPrecision(18, 2);

            // LimiteChequeEspecial na ContaCorrente
            modelBuilder.Entity<ContaCorrente>()
                .Property(cc => cc.LimiteChequeEspecial)
                .HasPrecision(18, 2);

            // Valor na RegistroTransacao
            modelBuilder.Entity<RegistroTransacao>()
                .Property(rt => rt.Valor)
                .HasPrecision(18, 2);

            // --- B. MAPEAMENTO DE HERANÇA E CHAVES ---

            // Mapeamento de Herança TPH (Table Per Hierarchy) na tabela 'Contas'
            modelBuilder.Entity<Conta>()
                        .HasDiscriminator<string>("TipoConta")
                        .HasValue<ContaCorrente>("CORRENTE")
                        .HasValue<Poupanca>("POUPANCA");

            // Define a Chave Primária de Conta
            modelBuilder.Entity<Conta>().HasKey(c => c.Numero);


            // --- C. MAPEAMENTO DE RELACIONAMENTOS ---

            // Relacionamento 1:N Cliente -> Contas
            modelBuilder.Entity<Cliente>()
                        .HasMany(c => c.Contas)
                        .WithOne(conta => conta.Titular)
                        .HasForeignKey(conta => conta.TitularId);


            // Relacionamento 1:N Conta -> RegistrosTransacao
            modelBuilder.Entity<Conta>()
                        .HasMany(c => c.Historico) // Coleção definida em Conta
                        .WithOne(t => t.Conta)
                        .HasForeignKey(t => t.ContaNumero);

            // Nota: Os relacionamentos 1:1 e N:N não foram incluídos aqui, 
            // pois você não está usando as classes adicionais.
        }
    }
}
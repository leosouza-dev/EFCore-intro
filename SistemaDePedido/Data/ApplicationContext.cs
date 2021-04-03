using Microsoft.EntityFrameworkCore;
using SistemaDePedido.Data.Configurations;
using SistemaDePedido.Domain;
using System;

namespace SistemaDePedido.Data
{
    public class ApplicationContext : DbContext
    {
        // podemos declarar como dbSet...
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public ApplicationContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CursoEFCore;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        // Ou definir no OnModelCreating...
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // forma manual para todas as classes...
            //modelBuilder.ApplyConfiguration(new ClienteConfiguration());

            // executa para todas classes que implementam o IEntityTypeConfiguration
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
        }
    }
}

﻿using Microsoft.EntityFrameworkCore;
using SistemaDePedido.Data.Configurations;
using SistemaDePedido.Domain;

namespace SistemaDePedido.Data
{
    public class ApplicationContext : DbContext
    {
        // podemos declarar como dbSet...
        public DbSet<Pedido> Pedidos { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb; Initial Catalog=CursoEFCore; Integrated Security=True");
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

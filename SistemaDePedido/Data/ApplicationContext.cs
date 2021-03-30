using Microsoft.EntityFrameworkCore;
using SistemaDePedido.Domain;
using System;
using System.Collections.Generic;
using System.Text;

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
            modelBuilder.Entity<Cliente>(p => {
                p.ToTable("Clientes");
                p.HasKey(p => p.Id); // chave primária
                p.Property(p => p.Nome).HasColumnType("VARCHAR(80)").IsRequired();
                p.Property(p => p.Telefone).HasColumnType("CHAR(11)"); // char recomendado para valores fixos
                p.Property(p => p.CEP).HasColumnType("CHAR(8)").IsRequired();
                p.Property(p => p.Estado).HasColumnType("CHAR(2)").IsRequired();
                p.Property(p => p.Cidade).HasMaxLength(60).IsRequired(); // nvarchar de 60

                p.HasIndex(i => i.Telefone).HasName("idx_cliente_telefone"); // índice - para mais performance em pesquisa no bd
            });

            modelBuilder.Entity<Produto>(p => {
                p.ToTable("Produtos");
                p.HasKey(p => p.Id); // chave primária
                p.Property(p => p.CodigoBarras).HasColumnType("VARCHAR(14)").IsRequired();
                p.Property(p => p.Descricao).HasColumnType("VARCHAR(60)");
                p.Property(p => p.Valor).IsRequired();
                p.Property(p => p.TipoProduto).HasConversion<string>();
            });

            modelBuilder.Entity<Pedido>(p => {
                p.ToTable("Pedidos");
                p.HasKey(p => p.Id); // chave primária
                p.Property(p => p.IniciadoEM).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd(); //gera a data/hora na hora da add o resgitro
                p.Property(P => P.Status).HasConversion<string>();
                p.Property(P => P.TipoFrete).HasConversion<int>();
                p.Property(p => p.Observacao).HasColumnType("VARCHAR(512)");

                p.HasMany(p => p.Itens)
                    .WithOne(p => p.Pedido)
                    .OnDelete(DeleteBehavior.Cascade); // Ao deletar pedido, seus itens são deletados
            });

            modelBuilder.Entity<PedidoItem>(p => { 
                p.ToTable("PedidoItens");
                p.HasKey(p => p.Id); // chave primária
                p.Property(p => p.Quantidade).HasDefaultValue(1).IsRequired(); // já cria com valor 1 se não for definido
                p.Property(p => p.Valor).IsRequired();
                p.Property(p => p.Desconto).IsRequired();
            });
        }
    }
}

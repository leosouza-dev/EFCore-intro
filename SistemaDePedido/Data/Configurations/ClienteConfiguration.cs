using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaDePedido.Domain;

namespace SistemaDePedido.Data.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes");
            builder.HasKey(p => p.Id); // chave primária
            builder.Property(p => p.Nome).HasColumnType("VARCHAR(80)").IsRequired();
            builder.Property(p => p.Telefone).HasColumnType("CHAR(11)"); // char recomendado para valores fixos
            builder.Property(p => p.CEP).HasColumnType("CHAR(8)").IsRequired();
            builder.Property(p => p.Estado).HasColumnType("CHAR(2)").IsRequired();
            builder.Property(p => p.Cidade).HasMaxLength(60).IsRequired(); // nvarchar de 60

            builder.HasIndex(i => i.Telefone).HasName("idx_cliente_telefone"); // índice - para mais performance em pesquisa no bd
        }
    }
}

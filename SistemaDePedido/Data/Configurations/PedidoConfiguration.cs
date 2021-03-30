using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaDePedido.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaDePedido.Data.Configurations
{
    public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("Pedidos");
            builder.HasKey(p => p.Id); // chave primária
            builder.Property(p => p.IniciadoEM).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd(); //gera a data/hora na hora da add o resgitro
            builder.Property(P => P.Status).HasConversion<string>();
            builder.Property(P => P.TipoFrete).HasConversion<int>();
            builder.Property(p => p.Observacao).HasColumnType("VARCHAR(512)");

            builder.HasMany(p => p.Itens)
                .WithOne(p => p.Pedido)
                .OnDelete(DeleteBehavior.Cascade); // Ao deletar pedido, seus itens são deletados
        }
    }
}

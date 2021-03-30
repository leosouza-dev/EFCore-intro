using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaDePedido.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaDePedido.Data.Configurations
{
    public class PedidoItemConfiguration : IEntityTypeConfiguration<PedidoItem>
    {
        public void Configure(EntityTypeBuilder<PedidoItem> builder)
        {
            builder.ToTable("PedidoItens");
            builder.HasKey(p => p.Id); // chave primária
            builder.Property(p => p.Quantidade).HasDefaultValue(1).IsRequired(); // já cria com valor 1 se não for definido
            builder.Property(p => p.Valor).IsRequired();
            builder.Property(p => p.Desconto).IsRequired();
        }
    }
}

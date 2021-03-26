using System;
using System.Collections.Generic;
using System.Text;
using SistemaDePedido.ValueObjects;

namespace SistemaDePedido.Domain
{
    public class Pedido
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public Cliente Cliente { get; set; } // prop. de navegação
        public DateTime IniciadoEM { get; set; }
        public DateTime FinalizadoEM { get; set; }
        public TipoFrete TipoFrete { get; set; }
        public StatusPedido Status { get; set; }
        public string Observacao { get; set; }
        public ICollection<PedidoItem> Itens { get; set; } // prop. de navegação
    }
}

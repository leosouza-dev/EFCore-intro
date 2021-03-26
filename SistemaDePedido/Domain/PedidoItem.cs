namespace SistemaDePedido.Domain
{
    public class PedidoItem
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; } // prop. de navegação
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; } // prop. de navegação
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
        public decimal Desconto { get; set; }
    }
}
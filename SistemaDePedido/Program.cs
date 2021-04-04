using Microsoft.EntityFrameworkCore;
using SistemaDePedido.Domain;
using SistemaDePedido.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaDePedido
{
    class Program
    {
        static void Main(string[] args)
        {
            using var db = new Data.ApplicationContext();
            //db.Database.Migrate(); // executa as migrações. Não usar em prod.
            var existe = db.Database.GetPendingMigrations().Any(); // checando se existe migração pendente

            //InserirDados();
            //InserirDadosEmMassa();
            //ConsultarDados();
            //CadastrarPedido();
            ConsultarPedidoCarregamentoAdiantado();
        }

        private static void InserirDados()
        {
            var produto = new Produto
            {
                Descricao = "Produto teste",
                CodigoBarras = "1234567891231",
                Valor = 10m,
                TipoProduto = ValueObjects.TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            using var db = new Data.ApplicationContext();

            // 4 opções para add um registro
            // indicado usar as 2 primeiras...

            // db.Produtos.Add(produto); // 1-forma comum
            // db.Set<Produto>().Add(produto); // 2-étodo genérico
            // db.Entry(produto).State = EntityState.Added; // 3-rastrando uma entidade
            db.Add(produto);// 4-direto pela instancia do contexto
            db.SaveChanges();
        }

        private static void InserirDadosEmMassa()
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste",
                CodigoBarras = "1234567891231",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            var cliente = new Cliente
            {
                Nome = "Leonardo Souza",
                CEP = "99999000",
                Cidade = "São Paulo",
                Estado = "SP",
                Telefone = "99000001111",
            };

            var listaCliente = new List<Cliente>()
            {
                new Cliente
                {
                    Nome = "Leonardo 2",
                    CEP = "99999000",
                    Cidade = "São Paulo",
                    Estado = "SP",
                    Telefone = "99000001111",
                },
                new Cliente
                {
                    Nome = "Leonardo 3",
                    CEP = "99999000",
                    Cidade = "São Paulo",
                    Estado = "SP",
                    Telefone = "99000001111",
                },
            };

            using var db = new Data.ApplicationContext();
            //db.AddRange(produto, cliente);
            //db.Set<Cliente>().AddRange(listaCliente);
            db.Clientes.AddRange(listaCliente);

            var registros = db.SaveChanges();
            Console.WriteLine($"Total Registro(s): {registros}");
        }

        private static void ConsultarDados()
        {
            using var db = new Data.ApplicationContext();
            //var consultaPorSintaxe = (from c in db.Clientes where c.Id>0 select c).ToList();
            var consultaPorMetodo = db.Clientes
                //.AsNoTracking() // se der um find não olha em memória, vai pro bd consultar
                .Where(p => p.Id > 0)
                .OrderBy(p => p.Id)
                .ToList();

            foreach (var cliente in consultaPorMetodo)
            {
                Console.WriteLine($"Consultando Cliente: {cliente.Id}");
                db.Clientes.Find(cliente.Id);
                //db.Clientes.FirstOrDefault(p => p.Id == cliente.Id);
            }
        }

        private static void CadastrarPedido()
        {
            using var db = new Data.ApplicationContext();

            var cliente = db.Clientes.FirstOrDefault();
            var produto = db.Produtos.FirstOrDefault();

            var pedido = new Pedido
            {
                ClientId = cliente.Id,
                IniciadoEM = DateTime.Now,
                FinalizadoEM = DateTime.Now,
                Observacao = "Pedido Teste",
                Status = StatusPedido.Analise,
                TipoFrete = TipoFrete.SemFrete,
                Itens = new List<PedidoItem>
                 {
                     new PedidoItem
                     {
                         ProdutoId = produto.Id,
                         Desconto = 0,
                         Quantidade = 1,
                         Valor = 10,
                     }
                 }
            };

            db.Pedidos.Add(pedido);

            db.SaveChanges();
        }

        private static void ConsultarPedidoCarregamentoAdiantado()
        {
            using var db = new Data.ApplicationContext();
            var pedidos = db
                .Pedidos
                .Include(p => p.Itens)
                    .ThenInclude(p => p.Produto)
                .ToList();

            Console.WriteLine(pedidos.Count);
        }
    }
}

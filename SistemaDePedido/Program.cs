using Microsoft.EntityFrameworkCore;
using SistemaDePedido.Domain;
using System;
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

            InserirDados();
            Console.WriteLine("Hello World!");
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
    }
}

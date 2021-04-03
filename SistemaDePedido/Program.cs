using Microsoft.EntityFrameworkCore;
using System;

namespace SistemaDePedido
{
    class Program
    {
        static void Main(string[] args)
        {
            // podemos criar um comando para executar as migrações ao rodar a aplicação...
            using var db = new Data.ApplicationContext();
            db.Database.Migrate(); // executa as migrações. Não usar em prod.

            Console.WriteLine("Hello World!");
        }
    }
}

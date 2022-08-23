using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console
{
   class Program
   {
      static void Main(string[] args)
      {
         var productFactory = new ProductFactory();
         var productNew = productFactory.CreateNewProduct("Brand New Product", 100,
            new List<long> { 1L }, opt=>opt.WithDescription("My cool description"));
         var productExported = productFactory.CreateExportedProduct("My Partner Product", 100, 5,
           new List<long> { 1L }, null);
            System.Console.ReadKey();
      }
   }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console
{
   public class Product
   {
      public string Name { get; private set; }
      public string Description { get; private set; }
      public long Price { get; private set; }
      public long Ranking { get; private set; }
      public IList<long> CategoryIds { get; private set; }
      public Product(string name, string description, long price, long ranking, IList<long> categoryIds)
      {
         if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required.", nameof(name));
         if (price <= 0)
            throw new ArgumentException("Price must be positive.", nameof(price));
         if (ranking < 0)
            throw new ArgumentException("Ranking must be positive.", nameof(ranking));
         if (!categoryIds.Any())
            throw new ArgumentException("Assign product to at least one category.");

         Name = name;
         Description = description;
         Price = price;
         Ranking = ranking;
         CategoryIds = categoryIds;
      }
   }

}
